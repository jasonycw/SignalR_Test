## How it works? 
```
 -------------------------------       ---------------------------------
| Server                        |     | Client                          |
|  ---------------------------  |     |  -----------------------------  |
| | Hub                       | |     | | JavaScript connect Hub      | |
| |    SendMethod()  <---------------------  connection send something| |
| |       call client receive------------->  on receive do something  | |
|  ---------------------------  |     |  -----------------------------  |
 -------------------------------       ---------------------------------
 ```

 ## Flow
 ```
       Server           |      Client
---------------------------------------------
1. MapHub in Startup   | 
2.                     |  Setup the connection
                        |   and events
3. OnConnectedAsync   <-- Connect to a hub
                        |
4. Send some events   --> On receive event,
                        |   do something
                        |  
4. Hub method trigger <-- Invoke hub method
                        |
5. OnDisconnectedAsync<-- End of life cycle
 ```
