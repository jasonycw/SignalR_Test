var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:11978/SomeHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveSomething", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + ": \"" + msg + "\"";
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function(){
    document.getElementById("sendButton").disabled = false;
    console.log("SignalR connection success");
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("SendSomething", "Test User", "Hello World").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});