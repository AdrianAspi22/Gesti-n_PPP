using System.ComponentModel;
using System.Reflection;

namespace GestionAsesoria.Operator.Shared.Constants.Permission
{
    public static class Permissions
    {
        [DisplayName("Users")]
        [Description("Users Permissions")]
        public static class Users
        {
            public const string Menu = "Permissions.Users.Menu";
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string Export = "Permissions.Users.Export";
            public const string Search = "Permissions.Users.Search";
        }

        [DisplayName("Research Group Coordinators")]
        [Description("Research Group Coordinators Permissions")]
        public static class ResearchGroupCoordinators
        {
            public const string Menu = "Permissions.ResearchGroupCoordinators.Menu";
            public const string View = "Permissions.ResearchGroupCoordinators.View";
            public const string Create = "Permissions.ResearchGroupCoordinators.Create";
            public const string Edit = "Permissions.ResearchGroupCoordinators.Edit";
            public const string Delete = "Permissions.ResearchGroupCoordinators.Delete";
            public const string ChangeState = "Permissions.ResearchGroupCoordinators.ChangeState";
            public const string Search = "Permissions.ResearchGroupCoordinators.Search";
        }


        [DisplayName("Research Groups")]
        [Description("Research Groups Permissions")]
        public static class ResearchGroups
        {
            public const string Menu = "Permissions.ResearchGroups.Menu";
            public const string View = "Permissions.ResearchGroups.View";
            public const string Create = "Permissions.ResearchGroups.Create";
            public const string Edit = "Permissions.ResearchGroups.Edit";
            public const string Delete = "Permissions.ResearchGroups.Delete";
            public const string ChangeState = "Permissions.ResearchGroups.ChangeState";
            public const string Search = "Permissions.ResearchGroups.Search";
        }

        [DisplayName("Research Areas")]
        [Description("Research Areas Permissions")]
        public static class ResearchAreas
        {
            public const string Menu = "Permissions.ResearchAreas.Menu";
            public const string View = "Permissions.ResearchAreas.View";
            public const string Create = "Permissions.ResearchAreas.Create";
            public const string Edit = "Permissions.ResearchAreas.Edit";
            public const string Delete = "Permissions.ResearchAreas.Delete";
            public const string ChangeState = "Permissions.ResearchAreas.ChangeState";
            public const string Search = "Permissions.ResearchAreas.Search";
        }

        [DisplayName("Research Lines")]
        [Description("Research Lines Permissions")]
        public static class ResearchLines
        {
            public const string Menu = "Permissions.ResearchLines.Menu";
            public const string View = "Permissions.ResearchLines.View";
            public const string Create = "Permissions.ResearchLines.Create";
            public const string Edit = "Permissions.ResearchLines.Edit";
            public const string Delete = "Permissions.ResearchLines.Delete";
            public const string ChangeState = "Permissions.ResearchLines.ChangeState";
            public const string Search = "Permissions.ResearchLines.Search";
        }

        [DisplayName("Document Types")]
        [Description("Document Types Permissions")]
        public static class DocumentTypes
        {
            public const string View = "Permissions.DocumentTypes.View";
            public const string Create = "Permissions.DocumentTypes.Create";
            public const string Edit = "Permissions.DocumentTypes.Edit";
            public const string Delete = "Permissions.DocumentTypes.Delete";
        }

        [DisplayName("Projects")]
        [Description("Projects Permissions")]
        public static class Projects
        {
            public const string Menu = "Permissions.Projects.Menu";
            public const string View = "Permissions.Projects.View";
            public const string Create = "Permissions.Projects.Create";
            public const string Edit = "Permissions.Projects.Edit";
            public const string Delete = "Permissions.Projects.Delete";
            public const string ChangeState = "Permissions.Projects.ChangeState";
            public const string Search = "Permissions.Projects.Search";
        }

        [DisplayName("Advisors")]
        [Description("Advisors Permissions")]
        public static class Advisors
        {
            public const string Menu = "Permissions.Advisors.Menu";
            public const string View = "Permissions.Advisors.View";
            public const string Create = "Permissions.Advisors.Create";
            public const string Edit = "Permissions.Advisors.Edit";
            public const string Delete = "Permissions.Advisors.Delete";
            public const string ChangeState = "Permissions.Advisors.ChangeState";
            public const string Search = "Permissions.Advisors.Search";
        }

        [DisplayName("Students")]
        [Description("Students Permissions")]
        public static class Students
        {
            public const string Menu = "Permissions.Students.Menu";
            public const string View = "Permissions.Students.View";
            public const string Create = "Permissions.Students.Create";
            public const string Edit = "Permissions.Students.Edit";
            public const string Delete = "Permissions.Students.Delete";
            public const string ChangeState = "Permissions.Students.ChangeState";
            public const string Search = "Permissions.Students.Search";
        }

        [DisplayName("Advisoring Contracts")]
        [Description("Advisoring Contracts Permissions")]
        public static class AdvisoringContracts
        {
            public const string Menu = "Permissions.AdvisoringContracts.Menu";
            public const string View = "Permissions.AdvisoringContracts.View";
            public const string Create = "Permissions.AdvisoringContracts.Create";
            public const string Edit = "Permissions.AdvisoringContracts.Edit";
            public const string Delete = "Permissions.AdvisoringContracts.Delete";
            public const string DesactivateAdvisoringContract = "Permissions.AdvisoringContracts.DesactivateAdvisoringContract";
            public const string Search = "Permissions.AdvisoringContracts.Search";
        }

        [DisplayName("Advisoring Requests")]
        [Description("Advisoring Requests Permissions")]
        public static class AdvisoringRequests
        {
            public const string Menu = "Permissions.AdvisoringRequests.Menu";
            public const string View = "Permissions.AdvisoringRequests.View";
            public const string Create = "Permissions.AdvisoringRequests.Respond";
            public const string Delete = "Permissions.AdvisoringRequests.Delete";
            public const string Search = "Permissions.AdvisoringRequests.Search";
        }


        [DisplayName("Thesis")]
        [Description("Thesis Permissions")]
        public static class Thesis
        {
            public const string Menu = "Permissions.Thesis.Menu";
            public const string View = "Permissions.Thesis.View";
            public const string Create = "Permissions.Thesis.Create";
            public const string Edit = "Permissions.Thesis.Edit";
            public const string Delete = "Permissions.Thesis.Delete";
            public const string ChangeState = "Permissions.Thesis.ChangeState";
            public const string Search = "Permissions.Thesis.Search";
        }

        [DisplayName("Actor Types")]
        [Description("Actor Types Permissions")]
        public static class ActorTypes
        {
            public const string Menu = "Permissions.ActorTypes.Menu";
            public const string View = "Permissions.ActorTypes.View";
            public const string Create = "Permissions.ActorTypes.Create";
            public const string Edit = "Permissions.ActorTypes.Edit";
            public const string Delete = "Permissions.ActorTypes.Delete";
            public const string ChangeState = "Permissions.ActorTypes.ChangeState";
            public const string Search = "Permissions.ActorTypes.Search";
        }

        [DisplayName("Actor")]
        [Description("Actors Permissions")]
        public static class Actors
        {
            public const string Menu = "Permissions.Actors.Menu";
            public const string View = "Permissions.Actors.View";
            public const string Create = "Permissions.Actors.Create";
            public const string Edit = "Permissions.Actors.Edit";
            public const string Delete = "Permissions.Actors.Delete";
            public const string ChangeState = "Permissions.Actors.ChangeState";
            public const string Search = "Permissions.Actors.Search";
        }

        [DisplayName("Group Member")]
        [Description("Group Members Permissions")]
        public static class GroupMembers
        {
            public const string Menu = "Permissions.GroupMembers.Menu";
            public const string View = "Permissions.GroupMembers.View";
            public const string Create = "Permissions.GroupMembers.Create";
            public const string Edit = "Permissions.GroupMembers.Edit";
            public const string Delete = "Permissions.GroupMembers.Delete";
            public const string ChangeState = "Permissions.GroupMembers.ChangeState";
            public const string Search = "Permissions.GroupMembers.Search";
        }

        [DisplayName("Master Data Value")]
        [Description("Master Data Values Permissions")]
        public static class MasterDataValues
        {
            public const string View = "Permissions.MasterDataValues.View";
        }

        [DisplayName("Google Calendars")]
        [Description("Google Calendars Permissions")]
        public static class GoogleCalendars
        {
            public const string Menu = "Permissions.GoogleCalendars.Menu";
            public const string View = "Permissions.GoogleCalendars.View";
            public const string Create = "Permissions.GoogleCalendars.Create";
            public const string Edit = "Permissions.GoogleCalendars.Edit";
            public const string Delete = "Permissions.GoogleCalendars.Delete";
        }



        //Identity
        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            public const string Menu = "Permissions.Roles.Menu";
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string Search = "Permissions.Roles.Search";
        }

        [DisplayName("Role Claims")]
        [Description("Role Claims Permissions")]
        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
            public const string Search = "Permissions.RoleClaims.Search";
        }

        [DisplayName("Preferences")]
        [Description("Preferences Permissions")]
        public static class Preferences
        {
            public const string ChangeLanguage = "Permissions.Preferences.ChangeLanguage";

            //TODO - add permissions
        }


        //OPCIONAL SI LO REQUIERE EL PROYECTO
        [DisplayName("Dashboards")]
        [Description("Dashboards Permissions")]
        public static class Dashboards
        {
            public const string View = "Permissions.Dashboards.View";
        }

        //PARA TAREAS EN SEGUNDO PLANO
        [DisplayName("Hangfire")]
        [Description("Hangfire Permissions")]
        public static class Hangfire
        {
            public const string View = "Permissions.Hangfire.View";
        }

        [DisplayName("Audit Trails")]
        [Description("Audit Trails Permissions")]
        public static class AuditTrails
        {
            public const string Menu = "Permissions.AuditTrails.Menu";
            public const string View = "Permissions.AuditTrails.View";
            public const string Export = "Permissions.AuditTrails.Export";
            public const string Search = "Permissions.AuditTrails.Search";
        }


        [DisplayName("Charges")]
        [Description("Charges Permissions")]
        public static class Charges
        {
            public const string Menu = "Permissions.Charges.Menu";
            public const string View = "Permissions.Charges.View";
            public const string Create = "Permissions.Charges.Create";
            public const string Edit = "Permissions.Charges.Edit";
            public const string Delete = "Permissions.Charges.Delete";
            public const string ChangeState = "Permissions.Charges.ChangeState";
        }

        ///More Permissions here

        /// <summary>
        /// Returns a list of Permissions.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permissions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permissions.Add(propertyValue.ToString()!);
            }
            return permissions;
        }
    }
}