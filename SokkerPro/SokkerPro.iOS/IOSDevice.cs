﻿using SokkerPro.iOS;
using System.Runtime.InteropServices;
using SokkerPro.Services;
using Foundation;
using System;
using Security;

[assembly: Xamarin.Forms.Dependency(typeof(IOSDevice))]
namespace SokkerPro.iOS
{

    public class IOSDevice : IDevice
    {
        //[DllImport("/System/Library/Frameworks/IOKit.framework/IOKit")]
        //private static extern uint IOServiceGetMatchingService(uint masterPort, IntPtr matching);

        //[DllImport("/System/Library/Frameworks/IOKit.framework/IOKit")]
        //private static extern IntPtr IOServiceMatching(string s);

        //[DllImport("/System/Library/Frameworks/IOKit.framework/IOKit")]
        //private static extern IntPtr IORegistryEntryCreateCFProperty(uint entry, IntPtr key, IntPtr allocator, uint options);

        //[DllImport("/System/Library/Frameworks/IOKit.framework/IOKit")]
        //private static extern int IOObjectRelease(uint o);

        public string GetIdentifier()
        {
            //string serial = string.Empty;
            //uint platformExpert = IOServiceGetMatchingService(0, IOServiceMatching("IOPlatformExpertDevice"));
            //if (platformExpert != 0)
            //{
            //    NSString key = (NSString)"IOPlatformSerialNumber";
            //    IntPtr serialNumber = IORegistryEntryCreateCFProperty(platformExpert, key.Handle, IntPtr.Zero, 0);
            //    if (serialNumber != IntPtr.Zero)
            //    {
            //        serial = NSString.FromHandle(serialNumber);
            //    }

            //    IOObjectRelease(platformExpert);
            //}

            //return serial;

            var query = new SecRecord(SecKind.GenericPassword);
            query.Service = NSBundle.MainBundle.BundleIdentifier;
            query.Account = "UniqueID";

            NSData uniqueId = SecKeyChain.QueryAsData(query);
            if (uniqueId == null)
            {
                query.ValueData = NSData.FromString(System.Guid.NewGuid().ToString());
                var err = SecKeyChain.Add(query);
                //if (err != SecStatusCode.Success && err != SecStatusCode.DuplicateItem)
                //    throw new Exception("Cannot store Unique ID");

                return query.ValueData.ToString();
            }
            else
            {
                return uniqueId.ToString();
            }
        }
    }
}
