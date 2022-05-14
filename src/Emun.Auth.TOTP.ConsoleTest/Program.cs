using Emun.Auth.TOTP;

Console.WriteLine("Hello");

var secretKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
var totp = new TOTPAuthenticator();
var setupCode = totp.GetStupCode("imun22", "imaun.ir", secretKey);

Console.WriteLine($"SetupCode is : {setupCode}");

var currPIN = totp.GetCurrentPIN(secretKey);
Console.WriteLine($"Current PIN is : {currPIN}");

var allPins = totp.GetAllPINS(secretKey, TimeSpan.FromMinutes(1));
foreach(var pin in allPins) {
    Console.WriteLine($"{pin}");
}

Console.ReadKey();
