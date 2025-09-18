using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.RoleFeatures;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Data
{
    public class DbInitializer
    {
        public static void Seed(Entities context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Name = "SuperAdmin" , Password = PasswordHasher.Hash("112233"), Mobile = "01000000000", RoleId = Role.SuperAdmin ,VerifyStatus = VerifyStatus.Approve ,IsActive = true}
                );
                context.SaveChanges();
            }

            var allRoles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
            var allFeatures = Enum.GetValues(typeof(Feature)).Cast<Feature>().ToList();

            var newRoleFeatures = new List<RoleFeature>();

            foreach (var role in allRoles)
            {
                var existingFeatures = context.RoleFeatures
                    .Where(rf => rf.RoleId == role)
                    .Select(rf => rf.Features)
                    .ToHashSet();

                var missingFeatures = allFeatures.Except(existingFeatures);

                newRoleFeatures.AddRange(
                    missingFeatures.Select(f => new RoleFeature
                    {
                        RoleId = role,
                        Features = f
                    })
                );
            }

            if (newRoleFeatures.Any())
            {
                context.RoleFeatures.AddRange(newRoleFeatures);
                context.SaveChanges();
            }
        }
    }
}
