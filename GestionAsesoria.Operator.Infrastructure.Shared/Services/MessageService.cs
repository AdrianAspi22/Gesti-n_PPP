using GestionAsesoria.Operator.Application.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace GestionAsesoria.Operator.Infrastructure.Shared.Services
{
    public class MessageService : IMessageService
    {
        private readonly Dictionary<string, JObject> _messagesCache = new();
        private readonly string _jsonFilePath;
        private readonly ILogger<MessageService> _logger;
        private readonly FileSystemWatcher _fileWatcher;

        public MessageService(IHostEnvironment hostEnvironment, ILogger<MessageService> logger, string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
            _messagesCache = new Dictionary<string, JObject>();
            _logger = logger;

            // Log para verificar la ruta del archivo
            _logger.LogInformation($"Cargando mensajes desde: {_jsonFilePath}");

            // Cargar los mensajes desde el archivo JSON
            LoadMessages();

            // Configurar el monitor de cambios en el archivo
            _fileWatcher = new FileSystemWatcher(Path.GetDirectoryName(_jsonFilePath))
            {
                Filter = Path.GetFileName(_jsonFilePath),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size
            };
            _fileWatcher.Changed += OnFileChanged;
            _fileWatcher.EnableRaisingEvents = true;
        }

        // Cargar mensajes desde el archivo JSON
        private void LoadMessages()
        {
            try
            {
                // Verificación de la existencia del archivo
                if (!File.Exists(_jsonFilePath))
                {
                    _logger.LogError($"El archivo de mensajes no se encontró en la ruta: {_jsonFilePath}");
                    throw new FileNotFoundException($"El archivo de mensajes no se encontró en la ruta: {_jsonFilePath}");
                }

                var jsonContent = File.ReadAllText(_jsonFilePath);
                var loadedMessages = JObject.Parse(jsonContent);

                _messagesCache.Clear();

                foreach (var category in loadedMessages.Properties())
                {
                    _messagesCache[category.Name] = category.Value as JObject ?? new JObject();
                }

                _logger.LogInformation("Mensajes cargados correctamente desde el archivo JSON.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar los mensajes desde el archivo JSON.");
                throw;
            }
        }

        // Detectar cambios en el archivo y recargar los mensajes
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                _logger.LogInformation("Archivo de mensajes modificado. Recargando...");
                LoadMessages();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recargar el archivo de mensajes.");
            }
        }

        // Obtener un mensaje basado en categoría, sección y clave
        public string GetMessage(string category, string section, string key)
        {
            if (!_messagesCache.ContainsKey(category))
            {
                return $"Categoría '{category}' no encontrada.";
            }

            var categoryMessages = _messagesCache[category];
            var sectionMessages = categoryMessages[section] as JObject;

            if (sectionMessages == null || !sectionMessages.ContainsKey(key))
            {
                return $"Clave '{key}' no encontrada en la sección '{section}' de la categoría '{category}'.";
            }

            return sectionMessages[key]?.ToString() ?? $"Mensaje vacío para la clave '{key}'.";
        }

        // Método dinámico para personalización de mensajes
        public string GetDynamicMessage(string category, string section, string key)
        {
            var messageTemplate = GetMessage(category, section, key);
            return string.Format(messageTemplate);
        }
    }
}
