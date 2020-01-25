Talamitam Bencode

Talamitam Bencode is a bencode encoder decoder for .net. I know that there are already bencoding libraries in .net, but I have a good reason. I was bored, and I wanted to try my hand using TDD to write something I can use for something else. So now I have this.

It's named after a mountain in the Philippines.

* Dependencies

nunit.3.12.0.nupkg
net45
nunit.framework.dll

* Tests

csc.exe /target:library /reference:C:\Users\Jon\csharp\talamitam\libs\nunit.framework.dll /out:TalamitamTests.dll src\TalamitamException.cs src\types\BencodeType.cs tests\types\BencodeTypeTests.cs src\Types\BencodeNumber.cs tests\Types\BencodeNumberTests.cs src\Types\BencodeString.cs tests\Types\BencodeStringTests.cs src\Types\BencodeList.cs tests\Types\BencodeListTests.cs src\Types\BencodeDictionary.cs tests\Types\BencodeDictionaryTests.cs src\Parser.cs tests\ParserTests.cs tests\BaseTests.cs

nunitlite-runner.exe TalamitamTests.dll

* Building

csc.exe /target:library /out:Talamitam.dll src\TalamitamException.cs src\types\BencodeType.cs src\Types\BencodeNumber.cs src\Types\BencodeString.cs src\Types\BencodeList.cs src\Types\BencodeDictionary.cs src\Parser.cs

* Compatibility

C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe

* Notes