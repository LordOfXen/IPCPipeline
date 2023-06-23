<img src="https://raw.githubusercontent.com/LordOfXen/IPCPipeline/main/logo_header.png">

# IPCPipeline (front-end for Cloudtoid Interprocess)

[<img src="https://img.shields.io/badge/nuget-v1.0.1-green">](https://www.nuget.org/packages/IPCPipeline/)
[<img src="https://img.shields.io/badge/License-MIT-blue">](LICENSE)
<img src="https://img.shields.io/badge/.net-%3E%206.0-blue">
<img src="https://img.shields.io/badge/.net%20core-%3E%203.1-blue">

Inter-process bi-directional communication pipeline (IPC) or (IPBDC) that can both write and read data at the same time. This uses [cloudtoid/interprocess](https://github.com/cloudtoid/interprocess) as its low-level backend API.

This project is based on [Cloudtoid/Interprocess](https://github.com/cloudtoid/interprocess) and is for the ones that do not want to spend time on writing a wrapper for their project to use that library.

# What can it do?
It wraps the necessary classes to only one class called **IPCPipeline** which you can use for both writing and reading in a channel.
We use [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) for sending and receiving data over Cloudtoid Interprocess library. This makes it easy to read any object or data in a subscriber.

Due to JSON and other circumstances, this library will slightly be slower than the original low-level library. So if speed is a concern for you (if your project is as sensitive as nanoseconds or less than 2 milliseconds), then do not use this project. Use the original, low-level version at https://github.com/cloudtoid/interprocess.

## Usage

This library supports .NET Core 3.1+ and .NET 6+.

### Create a Publisher and Send Message to Subscribers
Creating an instance of **IPCPipeline** with the name of the channel and access mode as a **publisher**:

```csharp
var pipe = new IPCPipeline("myChannel", PipeAccess.Write);
```

Send a message from the **publisher** to **subscribers**:
```csharp
var response = pipe.SendMessage(data);
if (response.IsSuccess)
{
  // ...
}
```

### Create a Subscriber and read messages coming from Publishers
Creating an instance of **IPCPipeline** with the name of the channel used to create the **publisher**:
```csharp
var pipe = new IPCPipeline("myChannel", PipeAccess.Read);
```

Receive messages from **publishers**:
```csharp
pipe.OnMessageReceived += (msgid, data) =>
{
  var data = data.ToObject<PipelineData>();
  Console.WriteLine("[{0}] Data \"{1}\" received.", data.SentAt, data.Name);
  
  return System.Threading.Tasks.Task.CompletedTask;
};
```

### Send Primitive Types rather than Objects
If you do not want to create custom classes, you can use the built-in primitive type data classes to send messages to subscribers.

Here's an example about sending a **string**:
```csharp
pipe.SendMessage("myStringMessageName", "the text you want to send");
```

Here's an example about sending a **bool**:
```csharp
pipe.SendMessage("myBoolMessageName", true);
```

Here's an example about sending a **float**:
```csharp
pipe.SendMessage("myFloatMessageName", 0.5f);
```

Here's an example about sending a byte array:
```csharp
pipe.SendMessage("myByteArrayMessageName", System.IO.File.ReadAllBytes("myImage.png")); // Make sure the buffer is big enough in subscriber's IPCPipeline class instance.
```

Receiving any of these values is as easy as shown above:
```csharp
pipe.OnMessageReceived += (msgid, data) =>
{
  var data = data.ToObject<PipelineData>();
  if (data.Name == "myStringMessageName")
  {
    var textData = data.ToObject<PipelineStringData>();
    Console.WriteLine("Received string: {0}", textData.Value);
  }
};
```

## Sample

To see a sample implementation of a publisher and a subscriber process, try out the following two projects. You can run them side by side and see them in action:

- [Publisher](src/PublisherTest/)
- [Subscriber](src/SubscriberTest/)

## More Information

The rest is almost the same as the original library, so check it at https://github.com/cloudtoid/interprocess.

## Discord Server

Join our Discord server at [here](https://discord.gg/deskasoft) to discuss about this or the main library and get help from other members like you.
