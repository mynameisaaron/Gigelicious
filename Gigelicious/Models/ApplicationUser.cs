﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gigelicious.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Follow> Followers  { get; set; }
        public ICollection<Follow> Followees  { get; set; }
        //Added UserNotifications - navigation class to replace "_context.UserNotifications.Add(userNotification)"
        public ICollection<UserNotification> UserNotifications  { get; set; }
        


        public ApplicationUser()
        {
            Followers = new Collection<Follow>();
            Followees = new Collection<Follow>();
            UserNotifications = new Collection<UserNotification>();
            
            
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            
                                               // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public void Notify(Notification notification)
        {
            UserNotifications.Add(new UserNotification(this,notification));
        }
    }
}