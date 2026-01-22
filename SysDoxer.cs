using System;  // this gives me access to basic runtime things like Console, Environment, DateTime
using System.Diagnostics; // this is needed for process ids, loaded DLLs etc.
using System.Net; // basic networking types, IP addrs, DNS etc
using System.Net.NetworkInformation; // this thing is adapter-level networking namespace
// remember, "using" is not equal to "include" file, "using" doesnt pull files
// "using" makes the types visible by name
// if IPAddress is there, try resolving it under System.Net!
using System.Text.Json;
// using System.Management;


class Programy // this is a container. C# requires code to live inside a type
               // the OS doesnt care, runtime does!
{
    static void Main() // this is the ENTRY POINT.
                       // static is used here because no object exists yet
    {
        //PrintSystemInfo();
        //PrintNetworkInfo();
        //PrintProcessInfo();
        //PrintLoadedDlls();

        while (true)
        {
            Console.Clear();
           
            Console.WriteLine("""
                  _____           _____                     
                 / ____|         |  __ \                    
                | (___  _   _ ___| |  | | _____  _____ _ __ 
                 \___ \| | | / __| |  | |/ _ \ \/ / _ \ '__|
                 ____) | |_| \__ \ |__| | (_) >  <  __/ |   
                |_____/ \__, |___/_____/ \___/_/\_\___|_|   
                         __/ |                              
                        |___/                         v1.0
                """);
            //Console.WriteLine("====System Scope ====");
            Console.Out.Flush();
            Console.WriteLine("--------------------- By Mr. BILRED");
            Console.WriteLine("1. Show System info");
            Console.WriteLine("2. Show Network Interfaces");
            Console.WriteLine("3. Show Current Process Info");
            Console.WriteLine("4. Show loaded DLLs");
            Console.WriteLine("5. Show Local IP, Subnet, and Gateway");
            Console.WriteLine("6. Show Public IP (requires internet)");
            Console.WriteLine("7. Exit");
            Console.WriteLine();
            Console.WriteLine("Select option: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    PrintSystemInfo();
                    break;

                case "2":
                    PrintNetworkInfo();
                    break;
                case "3":
                    PrintCurrentProcessInfo();

                    break;

                case "4":
                    PrintLoadedDlls();
                    break;

                case "5":
                    PrintLocalIP();
                    break;
                case "6":
                    PrintPublicIPAsync().GetAwaiter().GetResult();
                    break;
                case "7":
                    return;
            }
            Console.WriteLine();
            Console.Write("Print Enter to continue...");
            Console.ReadLine();
        }


    }

    static void PrintSystemInfo()
    {
        Console.WriteLine("===== System Info ====");
        Console.WriteLine($"Machine : {Environment.MachineName}"); // Enviroment is runtime fingerprinting
        Console.WriteLine($"User : {Environment.UserName}");
        Console.WriteLine($"OS : {Environment.OSVersion}");
        Console.WriteLine(""); // empty line cuz "cleanliness is important", hehe

        // for RAM (but there was a problem)

        //var searcher = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize, FreePhyscialMemory FROM Win32_OperatingSystem");
        //foreach (ManagementObject os in searcher.Get())
        //{
        //    ulong totalKb = (ulong)os["TotalVisibleMemorySize"];
        //    ulong freeKb = (ulong)os["FreePhysicalMemory"];

        //    Console.WriteLine($"Total RAM : {totalKb / 1024} MB");
        //    Console.WriteLine($"Free RAM  : {freeKb / 1024} MB");
        //}

    }

    static void PrintNetworkInfo()
    {
        Console.WriteLine("==== NetworkInfo====");

        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        // lemme tell u its meaning.

        // foreach means go through the collection one item at a time
        // Network.GetAllNetworkInterfaces() calls a static method, returns an array of NetworkInterface
        // each element is one network adapter
        // NetworkInterface nic means, I want a variable named nic, that can
        // hold ONE adapter at a time!
        // NetworkInterface is the type
        // nic is a temporary variable
        {  // i just learnt why this brace is important for foreach!
            Console.WriteLine($"Name : {nic.Name}");
            Console.WriteLine($"Type : {nic.NetworkInterfaceType}");
            Console.WriteLine($"MAC  : {nic.GetPhysicalAddress()}");  // notice the brackets, cuz its a method, not a property!
            Console.WriteLine($"Status : {nic.OperationalStatus}");
        }
    
    }
    static void PrintCurrentProcessInfo()
    {
        Console.WriteLine("==== CURRENT PROCESS INFO ====");
        Console.WriteLine($"PID : {Process.GetCurrentProcess().Id}");
        Console.WriteLine();
    }
    static void PrintLoadedDlls()
    {
        Process current = Process.GetCurrentProcess();
        // "Process" is a class from System.Diagnostics
        // "current" is an object instance, representing This running program
        Console.WriteLine($"Current Process: {current.ProcessName} (PID: {current.Id})");
        Console.WriteLine();
        Console.WriteLine("==== Loaded Dlls =====");

        foreach (ProcessModule module in current.Modules) 
            // ProcessModule is the wrapper around each loaded module
            // current.Modules, all DLLs and EXEs currently mapped in memory for this process
        {
            Console.WriteLine($"Name: {module.ModuleName} | Path: {module.FileName} ");

        }
    }

    static void PrintLocalIP()
    {
        Console.WriteLine("========IP Information==========");
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) // Ask windows to give me every NIC registered with NDIS
        {
            // care only about ACTIVE adapters
            if (nic.OperationalStatus != OperationalStatus.Up)
                continue;

            if (nic.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                continue;

            IPInterfaceProperties ipProps = nic.GetIPProperties(); // GetIPProperties() pulls the TCP/IP stack configuration for NIC

            foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
            // Unicast means real assigned IPs, no multicast, no broadcast junk
            { // IPv4 only
                if (addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) // AddressFamily.InterNetwork means IPv4
                {
                    Console.WriteLine($"Interface : {nic.Name}");
                    Console.WriteLine($"Local IP : {addr.Address}");
                    Console.WriteLine($"Subnet : {addr.IPv4Mask}");
             
                }
            }

            foreach (GatewayIPAddressInformation gw in ipProps.GatewayAddresses)
            {
                Console.WriteLine($"Gateway : {gw.Address}");
            }
           
        }
    }

    static async System.Threading.Tasks.Task PrintPublicIPAsync()
    // async is used because internet speed "can" vary, and prevents program blocking
    // async is NOT speed!
    {
        
        try
        {
            using HttpClient client = new HttpClient();
            // HttpClient is a class, "new HttpClient()" allocates memory, creates a real object, runs its constructor, and returns a reference
            // client is not the object, it holds the reference
            // its wrong to say, async makes code run in parallel

            string json = await client.GetStringAsync("https://api.ipify.org?format=json"); // this line starts the internet request, and when waiting, await pauses THIS method only

            using JsonDocument doc = JsonDocument.Parse(json); // JSON parsing
            JsonElement root = doc.RootElement;
            Console.WriteLine("====== Public Identity ==== ");
            Console.WriteLine($"IP       : {root.GetProperty("ip").GetString()}");
            Console.WriteLine("Source    : https://api.ipify.org?format=json");
            //Console.WriteLine($"ISP      : {root.GetProperty("org").GetString()}");
            //Console.WriteLine($"ASN      : {root.GetProperty("asn").GetString()}");
            //Console.WriteLine($"Country  : {root.GetProperty("country_name")}");
            //Console.WriteLine($"City     : {root.GetProperty("city").GetString()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Public IP Unavailable");
            Console.WriteLine($"Reason: {ex.GetType().Name}");
        }
    }
    
}