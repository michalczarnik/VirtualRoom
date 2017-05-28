# VirtualRoom

**Post values** <br/>
Add room - *url: localhost/api/roomapi/addroom*
```json
{
   "adminid":"6e0ee133-012c-4800-b7d2-68ea93ef15e0",
	"name":"TestRoomCange",
	"description":"TestDescription",
	"capacity":11,
	"users":[
		"6e0ee133-012c-4800-b7d2-68ea93ef15e1",
		"6e0ee133-012c-4800-b7d2-68ea93ef15e5",
		"6e0ee133-012c-4800-b7d2-68ea93ef15e4",
		"6e0ee133-012c-4800-b7d2-68ea93ef15e3",
		"6e0ee133-012c-4800-b7d2-68ea93ef15e2"
	],
  "inonuri":"uritoicon"
}
```

Change room - *url: localhost/api/roomapi/changeroom*
```json
{
	"roomid":"4ace5093-0df7-41e4-9e49-a5ff73646662",
   "adminid":"6e0ee133-012c-4800-b7d2-68ea93ef15e0",
	"name":"TestRoomCange",
	"description":"TestDescription",
	"capacity":11,
	"users":[
		"6e0ee133-012c-4800-b7d2-68ea93ef15e1",
		"6e0ee133-012c-4800-b7d2-68ea93ef15e5",
		"6e0ee133-012c-4800-b7d2-68ea93ef15e4",
		"6e0ee133-012c-4800-b7d2-68ea93ef15e3",
		"6e0ee133-012c-4800-b7d2-68ea93ef15e2"
	],
  "inonuri":"uritoicon"
}

```

Remove room - *url: localhost/api/roomapi/removeroom*
```json
{
	"roomid":"4ace5093-0df7-41e4-9e49-a5ff73646662"
}

```

**TODO:**<br/>
- [ ] User management (static class per session)
- [ ] Visual representation of room
- [ ] Change and add room utility (the cshtml one with js)
- [ ] Icon selection utility (cshtml and js one, and also in the logic)
