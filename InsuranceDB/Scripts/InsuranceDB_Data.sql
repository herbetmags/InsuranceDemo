﻿--USE [InsuranceDB]
--GO
--insert into [Role] values (NEWID(), 'Admin', 'Administrator Role', 0, '3fa85f64-5717-4562-b3fc-2c963f66afa6', GETDATE(), null, null)
--insert into [Role] values (NEWID(), 'Agent', 'Agent Role', 0, '3fa85f64-5717-4562-b3fc-2c963f66afa6', GETDATE(), null, null)
--insert into [Role] values (NEWID(), 'Client', 'Client Role', 0, '3fa85f64-5717-4562-b3fc-2c963f66afa6', GETDATE(), null, null)
--GO
--insert into [Status] values ('F7A62836-6590-4BB5-AE04-EA8B64ED0D2E', 'PENDING','Pending')
--insert into [Status] values ('BD72EE54-6CBF-4F3A-A6C8-A08C32BFAB6E', 'ACTIVE','Active')
--insert into [Status] values ('A72B3CC7-09BB-4325-8DB9-6D890889133E', 'SUSPENDED','Suspended')
--insert into [Status] values ('41366F7E-A3EA-4706-8C05-C339630E265C', 'TERMINATED','Terminated')
--GO
--insert into [User] values ('88BD7287-AD8E-45F2-83AB-21384F68C74B', 'adminuser', '@dm1np@ssw0rd', 'Admin', 'User', 'admin.user@demo.com', 'BD72EE54-6CBF-4F3A-A6C8-A08C32BFAB6E', '83234D23-C8CD-4212-A6E3-982DED4FF9CB', '88BD7287-AD8E-45F2-83AB-21384F68C74B', GETDATE(), null, null)
--insert into [User] values (NEWID(), 'agentuser', '@g3ntp@ssw0rd', 'Agent', 'User', 'agent.user@demo.com', 'BD72EE54-6CBF-4F3A-A6C8-A08C32BFAB6E', '6013797C-60CB-4473-B0DE-37AEFF2D5F51', '88BD7287-AD8E-45F2-83AB-21384F68C74B', GETDATE(), null, null)
--insert into [User] values (NEWID(), 'clientuser', 'cl13ntp@ssw0rd', 'Client', 'User', 'client.user@demo.com', 'BD72EE54-6CBF-4F3A-A6C8-A08C32BFAB6E', '1391F9DD-2288-4D26-9D8A-623AC3BDF82F', '88BD7287-AD8E-45F2-83AB-21384F68C74B', GETDATE(), null, null)
--GO