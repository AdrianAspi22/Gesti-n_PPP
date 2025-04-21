namespace GestionAsesoria.Operator.Shared.Static
{
    public class ReplyMessage
    {
        //GENERAL
        public const string MESSAGE_QUERY = "Consulta exitosa.";
        public const string MESSAGE_QUERY_EMPTY = "No se encontraron registros.";
        public const string MESSAGE_SAVE = "Se registró correctamente.";
        public const string MESSAGE_UPDATE = "Se actualizó correctamente.";
        public const string MESSAGE_DELETE = "Se eliminó correctamente.";
        public const string MESSAGE_DELETE_CONFIRM = "La eliminación de este registro ya no está disponible, ";
        public const string MESSAGE_EDIT_CONFIRM = "La actualización de este registro ya no está disponible,  ";
        public const string MESSAGE_ACTIVO = "Se activó correctamente.";
        public const string MESSAGE_DESACTIVO = "Se desactivó correctamente.";
        public const string MESSAGE_EXISTS = "El registro ya existe.";
        public const string MESSAGE_ACTIVATE = "El registro ha sido activado.";
        public const string MESSAGE_TOKEN = "Token generado correctamente.";
        public const string MESSAGE_TOKEN_ERROR = "El usuario y/o contraseña es incorrecta, compruébala.";
        public const string MESSAGE_VALIDATE = "Errores de validación.";
        public const string MESSAGE_FAILED = "Operación fallida.";
        public const string MESSAGE_EXCEPTION = "Hubo un error inesperado, comunicarse con el administrador (admin@gmail.com).";
        public const string MESSAGE_GOOGLE_ERROR = "Su cuenta no se encuentra registrada en el sistema.";
        public const string MESSAGE_AUTH_TYPE_GOOGLE = "Por favor, ingrese con la opción de Google.";
        public const string MESSAGE_AUTH_TYPE = "Su cuenta no se encuentra registrada en el sistema.";
        public const string MESSAGE_AUTH_INACTIVE = "Su cuenta no tiene acceso comuniquese con el admistrador del sistema.";
        public const string MESSAGE_REGISTER_NOT_UNIQUE = " ingresado ya está en uso. Por favor, elija un ";
        public const string MESSAGE_VALIDATE_FILE_TYPE_IMAGE = "El archivo no es una imagen válida.";
        public const string MESSAGE_VALIDATE_FILE_TYPE_FILE = "Este archivo no es válido. ";
        public const string MESSAGE_VALIDATE_FILE_SIZE_FILE = "El archivo excede el tamaño máximo permitido.";
        public const string MESSAGE_CHANGE_PERFIL_ADMIN_STATE = "No se puede cambiar el estado del perfil de administrador.";
        public const string MESSAGE_INVALID_TENANT = "El dominio es inválido";
        public const string MESSAGE_EMAIL_CORRECT = "El Correo se envió con éxito";
        public const string MESSAGE_EMAIL_ERROR = "Error al enviar el correo";
        public const string MESSAGE_SUCCESS = "Consulta exitosa";


        //ACTOR
        public const string MESSAGE_ADVISOR_NOT_EXISTS = "El actor seleccionado como asesor no existe. Por favor, verifique la información e intente nuevamente";
        public const string MESSAGE_ACTOR_ASESOR_NO_EXISTS = "El actor seleccionado como asesor no existe. Por favor, verifique la información e intente nuevamente";
        public const string MESSAGE_DUPLICATE_COORDINATOR = "El grupo de investigación seleccionado ya cuenta con un coordinador. Por favor seleccione otro";


        //GroupMember
        public const string MESSAGE_GroupMember_EXISTS = "El Actor ya es Miembro del Grupo de Investigación";
        public const string MESSAGE_GroupMember_COORDINATOR_OTHER_RESEARCHGROUP = "El Coordinador ya pertenece a otro Grupo de Investigación";
        public const string MESSAGE_GroupMember_ERROR = "Error al Registrar al Miembro de Grupo";
        public const string MESSAGE_LISTGroupMember_ERROR = "Error al obtener la Lista de los Miembros de Grupo";


        //RESEARCHGROUP
        public const string MESSAGE_RESEARCHGROUP_NO_EXISTS = "El Grupo de Investigación especificado No Existe";
        public const string MESSAGE_LISTRESEARCHGROUP_ERROR = "Error al obtener la Lista de los Grupos de Investigación";
        public const string MESSAGE_LISTACRONYMRESEARCHGROUP_ERROR = "Error al obtener la Lista de Acrónimos de los Grupos de Investigación";


        //RESEARCHAREA
        public const string MESSAGE_LISTRESEARCHAREA_ERROR = "Error al obtener la Lista de las Áreas de Investigación";


        //RESEARCHLINE
        public const string MESSAGE_LISTRESEARCHLINE_ERROR = "Error al obtener la Lista de las Líneas de Investigación";


        //THESIS
        public const string MESSAGE_LISTTHESIS_ERROR = "Error al obtener la Lista de las Tesis";


        //STUDENT
        public const string MESSAGE_STUDENT_EXISTS_GROUP = "El Estudiante ya pertenece al Grupo de Investigación";
        public const string MESSAGE_STUDENT_NO_EXISTS = "El Estudiante especificado No Existe";


        //AdvisoringContract
        public const string MESSAGE_AdvisoringContract_NOT_EXISTS = "El Contrato de Asesoría especificado No Existe";
        public const string MESSAGE_AdvisoringContract_DESACTIVE = "El Contrato de Asesoría ha sido desactivado";
        public const string MESSAGE_AdvisoringContract_SUCCESS = "El Contrato de Asesoría se ha creado exitosamente y se ha enviado la solicitud al docente";
        public const string MESSAGE_AdvisoringContract_ERROR = "Ocurrió un error inesperado al crear el contrato de asesoría. Por favor, intente nuevamente";
        public const string MESSAGE_AdvisoringContract_ANSWER = "La respuesta al contrato de asesoría se ha registrado exitosamente";
        public const string MESSAGE_AdvisoringContract_ANSWER_ERROR = "Ocurrió un error inesperado al responder al contrato de asesoría. Por favor, intente nuevamente";
        public const string MESSAGE_LISTCONTRACT_ERROR = "Error inesperado al obtener la lista de Contratos de asesoría";

    }
}