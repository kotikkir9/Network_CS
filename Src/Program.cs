// See https://aka.ms/new-console-template for more information
using Netværk;

// var message1 = "82 19 43 2 102 hello 1";
// var message2 = "34 2 6 88 46 yo 3 99";

// Server server = new Server(5000);
// Client kim = new Client("Kim", 5000, message1);
// Client jong = new Client("Jong", 5000, message2);

// new Thread(server.Start).Start();

// Thread.Sleep(2000);
// new Thread(kim.Start).Start();

// Thread.Sleep(2000);
// new Thread(jong.Start).Start();


// while(true) {
//     System.Console.ReadLine();
//     if(server.ShutDown())
//     {
//         break;
//     }
// }

new LiveClient(5000);