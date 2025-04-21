//using GestionAsesoria.Operator.Domain.Entities.Identity;
//using GestionAsesoria.Operator.Infrastructure.Persistence.Contexts;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GestionAsesoria.Operator.Infrastructure.Persistence.Extensions
//{
//    public static class UserExtensions
//    {
//        public static async Task<List<AcademicUser>> LoadRecursively(this IQueryable<AcademicUser> query, ApplicationDbContext context)
//        {
//            var result = await query.ToListAsync();

//            foreach (var user in result)
//            {
//                await context.Entry(user).Collection(u => u.Subordinates).LoadAsync();
//                //var subordinates = await LoadRecursively(context.Users.Where(u => u.UserId == user.Id), context);
//                //user.Subordinates = subordinates;
//            }

//            return result;
//        }
//    }
//}
