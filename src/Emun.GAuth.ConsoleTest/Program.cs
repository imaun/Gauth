using Emun.GAuth;

Console.WriteLine("Hello");

var secretKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
var gAuth = new GoogleAuthenticator();
var setupCode = gAuth.GetStupCode("imun22", "imaun.ir", secretKey);

Console.WriteLine($"SetupCode is : {setupCode}");

var currPIN = gAuth.GetCurrentPIN(secretKey);
Console.WriteLine($"Current PIN is : {currPIN}");

var allPins = gAuth.GetAllPINS(secretKey, TimeSpan.FromMinutes(1));
foreach(var pin in allPins) {
    Console.WriteLine($"{pin}");
}

Console.ReadKey();
