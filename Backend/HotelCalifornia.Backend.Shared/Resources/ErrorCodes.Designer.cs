﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelCalifornia.Backend.Shared.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorCodes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorCodes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TokanPages.Backend.Shared.Resources.ErrorCodes", typeof(ErrorCodes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Access denied.
        /// </summary>
        public static string ACCESS_DENIED {
            get {
                return ResourceManager.GetString("ACCESS_DENIED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Requested article does not exists.
        /// </summary>
        public static string ARTICLE_DOES_NOT_EXISTS {
            get {
                return ResourceManager.GetString("ARTICLE_DOES_NOT_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to save file on Azure Blob Storage.
        /// </summary>
        public static string CANNOT_SAVE_TO_AZURE_STORAGE {
            get {
                return ResourceManager.GetString("CANNOT_SAVE_TO_AZURE_STORAGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This email address is already used.
        /// </summary>
        public static string EMAIL_ADDRESS_ALREADY_EXISTS {
            get {
                return ResourceManager.GetString("EMAIL_ADDRESS_ALREADY_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected error.
        /// </summary>
        public static string ERROR_UNEXPECTED {
            get {
                return ResourceManager.GetString("ERROR_UNEXPECTED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The string must be BASE64.
        /// </summary>
        public static string INVALID_BASE64 {
            get {
                return ResourceManager.GetString("INVALID_BASE64", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Requested subscriber does not exists.
        /// </summary>
        public static string SUBSCRIBER_DOES_NOT_EXISTS {
            get {
                return ResourceManager.GetString("SUBSCRIBER_DOES_NOT_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Requested user does not exists.
        /// </summary>
        public static string USER_DOES_NOT_EXISTS {
            get {
                return ResourceManager.GetString("USER_DOES_NOT_EXISTS", resourceCulture);
            }
        }
    }
}
