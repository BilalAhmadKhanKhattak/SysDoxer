# SysDoxer
A lightweight C#/.NET console tool that explores system-level information such as process identity, loaded modules (DLLs), local IP addresses, and network interfaces — demonstrating how managed code can still directly interact with the operating system.

<img width="757" height="316" alt="image" src="https://github.com/user-attachments/assets/c241cc9f-6004-48d7-b268-76557ea27431" />

# SysDoxer
**SysDoxer** is a lightweight **C# / .NET console-based system exploration tool** that demonstrates how managed code can interact with operating system components such as processes, loaded modules (DLLs), and network interfaces.

This project is driven by **curiosity, self-learning, and system-level understanding**, not academic requirements.

actually, in some sense I was helping somebody else, in some way, but when I saw visual studio, dotnet and searched a little about C#, it really got my attention
Though I have worked on Python, you may say, and it leads in some domains really good, but, C#, I can say, it HAS THE GUTS OF FREEDOM.
like, in just some "not so long code" you can talk to the system!
and the other feature which got my attention was
One code, Use anywhere.
Though "this" specific program may not work on other operating systems, but still, C# do have some really good advantages.
Although, C# has nothing to do with my academic semester, atleast yet.
but you know what! Curiosity, Self Teaching and Self Driven Quality is important.... For me? Yeah it IS VERY IMPORTANT

This program isn't "THAT" deep, yet it may provide a glimpse of touching the system stuff, like DLLs, basic system info, Local IP address (even without internet, LAN IP still exists), Network Interfaces. 

---

## 🔍 Features

- Display basic **system information**

  <img width="385" height="142" alt="image" src="https://github.com/user-attachments/assets/549bb78c-8d72-42f6-81ae-fe72c54bdab9" />

- Enumerate **network interfaces**
  
- Show **local IP address, subnet mask, and gateway**
- Inspect the **current running process**
- List **loaded DLLs/modules**
- Fetch **public IP address** (when internet is available) Yeah, This one IS NOT SYSTEM LEVEL, Okay? Must Understand the difference between Private IP and Public IP

---

## What This Project May Teach

- How .NET exposes system internals safely
- How processes and modules are inspected at runtime
- How network interfaces and IP addressing actually work
- Why local networking still functions without internet

This is **educational tooling**, not a replacement for professional diagnostics.

---

## Requirements

- .NET SDK
- Windows OS

> While .NET is cross-platform, I would recommend Windows OS for using this tool, and not Linux nor MacOS, because it contains Platform-specific APIs

---

## Running the Tool

Head to the folder where SysDoxer.cs is located, and on the file explorer's address bar, type "cmd", to open Command Prompt
<img width="207" height="37" alt="image" src="https://github.com/user-attachments/assets/a2aa9ee5-3d4c-4aad-893e-4dabdc66c0f7" />

Run this:
```bash

dotnet run SysDoxer.cs
```

<img width="406" height="112" alt="image" src="https://github.com/user-attachments/assets/98238549-7144-4243-90f3-2eabc94f5d81" />


Then Enjoy using SysDoxer!
<img width="425" height="321" alt="image" src="https://github.com/user-attachments/assets/732aabd3-7650-4c04-a36a-edf3f8e565d8" />


Author:
Bilal Ahmad Khan Also Known As Mr. BILRED

