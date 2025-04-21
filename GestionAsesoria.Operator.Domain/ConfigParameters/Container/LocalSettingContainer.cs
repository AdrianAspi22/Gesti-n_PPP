using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;

namespace GestionAsesoria.Operator.Domain.ConfigParameters.Container
{
    public class LocalSettingContainer
    {
        // Lista de contenedores de configuración globales

        private static List<SettingsContainer> globalSettingsContainers = new List<SettingsContainer>();
        private static readonly object syncLock = new object();

        private LocalSettingContainer() { }

        // Método para reiniciar (actualizar) una configuración existente en la lista
        public static void UpdateOrAddSettingContainer(SettingsContainer settingsContainer)
        {
            lock (syncLock)
            {
                // Agregar el nuevo contenedor
                globalSettingsContainers.Add(settingsContainer);
            }
        }

        // Método para obtener una configuración específica
        public static SettingsContainer Get()
        {

            var setting = globalSettingsContainers.FirstOrDefault();
            if (setting == null)
            {
                throw new InvalidOperationException("No hay configuraciones disponibles.");
            }
            return setting;
        }
    }
}
