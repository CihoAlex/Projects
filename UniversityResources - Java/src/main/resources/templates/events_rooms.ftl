<html>
<head>
  <title>University Resources</title>
</head>
</body>
<p>Schedule ID: ${schedule}</p>
<p>Events:</p>
<ul>
	<#list ev as event>
	<#assign type=event.getType()/>
		<li> ${event.getName()} - Size: ${event.getSize()} - Start: ${event.getStartTime()} - End: ${event.getEndTime()} - Type: ${event.getType()} - <#if type=="EVENT_COURSE"> ${event.getVideo()} <#elseif type=="EVENT_LABORATOR"> ${event.getOS()} </#if></li>
	</#list>
</ul>
<p>Rooms:</p>
<ul>
	<#list ro as room>
	<#assign type=room.getType()/>
		<li> ${room.getName()} - Size: ${room.getCap()} - Type: ${room.getType()} - <#if type=="ROOM_LECTURE_HALL"> ${room.getVideo()} <#elseif type=="ROOM_COMPUTER_LAB"> ${room.getOS()} </#if></li>
	</#list>
</ul>
</body>
</html>
