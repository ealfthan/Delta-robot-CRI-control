using System;
using Delta_robotin_ohjaus;
using log4net;
using static Delta_robotin_ohjaus.HardwareProtocolClient;


double standardVel = 100;

log4net.Config.BasicConfigurator.Configure();
HardwareProtocolClient criController = new HardwareProtocolClient();

criController.IPAddress = "192.168.3.11"; //192.168.3.11 || locahost
criController.Port = 3920; // 3920 || 3921

criController.Connect();

if (!criController.WaitForConnection())
{
    Console.WriteLine("Connection failed exiting");
    return;
}

// HUOM! 5mm < z < 135mm

criController.MoveToCoord(motionType.Cart, 100, -100, 70, standardVel);
if (!criController.WaitForMove()) { return; }

criController.MoveToCoord(motionType.Cart, -100, -100, 70, standardVel);
if (!criController.WaitForMove()) { return; }

criController.MoveToCoord(motionType.Cart, -100, 100, 70, standardVel);
if (!criController.WaitForMove()) { return; }

criController.MoveToCoord(motionType.Cart, 100, 100, 70, standardVel);
if (!criController.WaitForMove()) { return; }

Thread.Sleep(5000);

criController.Disconnect();