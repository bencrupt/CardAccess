# CardAccess
CardAccess is a hobby project aimed to support Kalmar Maker Space to make sure that no unauthorized use of machines and tools takes place. The idea is that an RFID tag that belongs to a member is read by the machine or tool, and if access has been granted, a relay is turned on, powering the machine or tool.

The equpment used is a Raspberry Pi with DietPi (include link) as operating system. A program (source code to be found in this repository) polls an RFID reader (include link to used reader) and turns a relay (include link to used relay) on or off, depending on the tag.
## Design Principles
There will be a setup of a Raspberry Pi, RFID reader and relay by every machine or tool that needs to be authorized. It is possible to connect the Pi to a local network and it is also possible to access the Internet. However, as of now there is no local network implemented, that is why resources at the Internet may be more suitable to use at this time.

Even though the is a connection problem with external network, the function must still be there. Thus we cannot rely upon constant Internet connection, it has to be taken into account that it is intermittent.

Currently there are no Internet resources possessed by Kalmar Maker Space, but there is a budget to setup a simple virtual server to be used and accessible from anywhere.

The setup will be headless by default. Possibly there will be one setup with connected keyboard and monitor where administrative tasks may take place, but it would be better this could be handled externally somehow.

Members of auhtorizations are today managed by using a spreadsheet on DropBox. This is not an optimal solution, it is tricky to maintain and to do email announcements is a nightmare. It would be great if a suitable system to manage members could be implemented, and if so, the authorization of a machine can be integrated to this system.
## Roadmap
### First implementation
The first implementation will be a stand-alone Pi with no connection to the outer world. The RFID tag can carry 48 characters of information. If each machine (less than 48) is given an identity of one character the tag can carry the information if that machine is authorized or not. There will be a character identifing the tag as an administrative tag (which may or may not have access to a machine). If an administrative tag is connected to a machine and it is replaced by an unauthorized tag within a time period (a few seconds), the new tag will become authorized. This allows a disconnected system with no need of user interface through monitor. One station might need a monitor and keybord to perform administrative tasks, for example to clear all accesses for a tag, och to grant administrative rights to a tag.

## Second implementation
T.B.D.
