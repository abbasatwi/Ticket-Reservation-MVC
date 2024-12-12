1- restore nugget packages


2- adjust db string in app settings



3- update database in package manager console 


4- run this script : 
USE [Project_Advanced-test]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'571151d4-3b42-4bc9-ab74-7eb74fd77864', N'Admin', N'ADMIN', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'fddd3b60-d983-4e36-94ea-5ab07e3549d2', N'User', N'USER', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'22c24bde-c5ca-4dd0-bda8-19cd893137ef', N'abbasatwi.2002@gmail.com', N'ABBASATWI.2002@GMAIL.COM', N'abbasatwi.2002@gmail.com', N'ABBASATWI.2002@GMAIL.COM', 0, NULL, N'EY6EIM5ALUERAX4ETSCRF3ORJFVSQ6NP', N'8ef4fe76-bcde-4539-9b2b-02aa1fb7e007', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2583c9fa-583d-48af-bc6a-c234b8a56d22', N'atwiabbas974@gmail.com', N'ATWIABBAS974@GMAIL.COM', N'atwiabbas974@gmail.com', N'ATWIABBAS974@GMAIL.COM', 0, NULL, N'N3USXXAEMFUUOXEKS6653O6PQQXFF7WC', N'c2522527-daad-48db-a175-f1634adbdbd2', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'767d4b2e-bf06-432a-8c9b-2892c564ac2d', N'abbasatwi1999@gmail.com', N'ABBASATWI1999@GMAIL.COM', N'abbasatwi1999@gmail.com', N'ABBASATWI1999@GMAIL.COM', 0, NULL, N'M7HKAVNFVF3T5GYEUUWDTS3DEGYX73FA', N'957404a2-70a8-452d-8a68-5da69ecab877', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'81be3901-cdd3-424c-891a-e028fa654516', N'aline@gmail.com', N'ALINE@GMAIL.COM', N'aline@gmail.com', N'ALINE@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEDmgiFfTSw+/Ub4V7thd3InmKY8ApxkBbExqfyMSkvbPYwHw7flJZQyykOfE+1dujw==', N'Q5IRISBM76FWUGPZOAP4G4FKMEAIUTA6', N'388b33ae-107d-4c26-bd07-262cb7ac7394', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'a86052a4-f68e-483d-b0f1-9b09ce6b7c74', N'admin@admin.com', N'ADMIN@ADMIN.COM', N'admin@admin.com', N'ADMIN@ADMIN.COM', 0, N'AQAAAAIAAYagAAAAEJ+eIfZPqtAnmqxzCEa7LL6nQjx/DMvqz0OQj4LNT+ZSdaSFDXgM8vC0IGXdNDRm0g==', N'VVTMINXPEZ7VQHR6IZXU5MFTMYZTURHX', N'53ca0127-2e46-4da7-821f-7993aa942b4a', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'a9b1632f-c1ee-4613-88af-d0fdcdff4d32', N'abbas@gmail.com', N'ABBAS@GMAIL.COM', N'abbas@gmail.com', N'ABBAS@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEBrVdufWxxfw2L5ENHIMzBMF+bnudiN0cZTkN+66TTMZdWr1mMN6HyW43JLiqfjhew==', N'RS75A6DVQMC6M7KSMIS6TOILK7MHIRPP', N'5959e034-2048-4583-bc9d-efef96dfc907', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a86052a4-f68e-483d-b0f1-9b09ce6b7c74', N'571151d4-3b42-4bc9-ab74-7eb74fd77864')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'81be3901-cdd3-424c-891a-e028fa654516', N'fddd3b60-d983-4e36-94ea-5ab07e3549d2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a9b1632f-c1ee-4613-88af-d0fdcdff4d32', N'fddd3b60-d983-4e36-94ea-5ab07e3549d2')
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'106385045218429763133', N'Google', N'767d4b2e-bf06-432a-8c9b-2892c564ac2d')
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'108073998752436166972', N'Google', N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'113470788926143468399', N'Google', N'2583c9fa-583d-48af-bc6a-c234b8a56d22')
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [ParentCategoryId], [Country]) VALUES (1, N'Champions League', 5, N'Europe')
INSERT [dbo].[Category] ([Id], [Name], [ParentCategoryId], [Country]) VALUES (5, N'European Cups', NULL, N'Europe')
INSERT [dbo].[Category] ([Id], [Name], [ParentCategoryId], [Country]) VALUES (6, N'Europa League', 5, N'Europe')
INSERT [dbo].[Category] ([Id], [Name], [ParentCategoryId], [Country]) VALUES (8, N'Spanish', NULL, N'Spain')
INSERT [dbo].[Category] ([Id], [Name], [ParentCategoryId], [Country]) VALUES (9, N'English', NULL, N'UK')
INSERT [dbo].[Category] ([Id], [Name], [ParentCategoryId], [Country]) VALUES (14, N'LaLiga', 8, N'Spain')
INSERT [dbo].[Category] ([Id], [Name], [ParentCategoryId], [Country]) VALUES (15, N'Copa Del Rey', 8, N'Spain')
INSERT [dbo].[Category] ([Id], [Name], [ParentCategoryId], [Country]) VALUES (16, N'Premier League', 9, N'UK')
INSERT [dbo].[Category] ([Id], [Name], [ParentCategoryId], [Country]) VALUES (17, N'FA Cup', 9, N'UK')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Stadium] ON 

INSERT [dbo].[Stadium] ([Id], [Name], [Description], [Capacity], [Location], [PicUrl], [BackPicUrl]) VALUES (1, N'Santiago Bernabeu', N'The Santiago Bernabéu is a renowned football stadium located in Madrid, Spain. It is the home stadium of Real Madrid, one of the most successful football clubs in the world. The stadium is known for its rich history, iconic architecture, and hosting major football events.', 100000, N'Madrid, Spain', N'52d25337-b9b6-4529-be85-676bb5190c86.jpg', N'c1876ae9-009a-4a87-b4a1-e321b7e2e20f.jpg')
INSERT [dbo].[Stadium] ([Id], [Name], [Description], [Capacity], [Location], [PicUrl], [BackPicUrl]) VALUES (2, N'Camp Nou', N'The Camp Nou is a prestigious football stadium situated in Barcelona, Spain. It serves as the home ground for FC Barcelona, one of the most celebrated football clubs globally. It stands as the largest stadium in Spain and Europe. The Camp Nou is renowned for its electrifying atmosphere during matches, as well as its architectural significance and illustrious history in football.', 105000, N'Barcelona, Spain', N'453f8389-6175-40f9-9c09-ce56d45f018b.jpg', N'413e2583-62b6-4180-b741-2f3ef8490cef.jpg')
INSERT [dbo].[Stadium] ([Id], [Name], [Description], [Capacity], [Location], [PicUrl], [BackPicUrl]) VALUES (4, N'Anfield', N' Anfield is the historic home stadium of Liverpool Football Club, located in Liverpool, England. Renowned for its electrifying atmosphere on matchdays, Anfield has been witness to countless iconic moments in football history, making it one of the most revered stadiums in the sport.', 70000, N'Liverpool, Uk', N'7cb838b2-98ed-4a42-bcda-d76f45751a0c.jpg', N'1e14d5e8-399e-4cc3-b724-9c374c0d53eb.jpg')
INSERT [dbo].[Stadium] ([Id], [Name], [Description], [Capacity], [Location], [PicUrl], [BackPicUrl]) VALUES (5, N'Allianz Arena', N' The Allianz Arena stands as a prominent landmark in Munich, Germany, serving as the home stadium for both FC Bayern Munich and TSV 1860 Munich. Its distinctive illuminated exterior, capable of changing colors, adds to its allure. This iconic venue hosts thrilling football matches and other major events, embodying the city''s sporting spirit and architectural innovation.', 97000, N'Munich, Germany', N'fe174fe6-0b16-4041-8c04-3965d8552ffa.jpg', N'e282f648-e404-4971-8f16-22b649c65a82.jpg')
INSERT [dbo].[Stadium] ([Id], [Name], [Description], [Capacity], [Location], [PicUrl], [BackPicUrl]) VALUES (6, N'Old Trafford', N' Old Trafford is the iconic home stadium of Manchester United, located in Greater Manchester, England. Steeped in history and tradition, it''s one of the most storied football grounds globally, witnessing legendary moments and hosting millions of passionate fans over the years.', 76000, N'Manchester, UK', N'834449ce-72a3-42f8-bc8e-4771645c6113.jpg', N'effbbc1f-5044-4681-bdb3-4bde2a1bf1d8.jpg')
INSERT [dbo].[Stadium] ([Id], [Name], [Description], [Capacity], [Location], [PicUrl], [BackPicUrl]) VALUES (7, N'Parc Des Princes', N'The Parc des Princes is the home stadium of Paris Saint-Germain (PSG), situated in Paris, France. With its modern facilities and vibrant atmosphere, it serves as a fortress for PSG, hosting thrilling football matches and showcasing the club''s ambition on the European stage.', 80000, N'Paris, France', N'fc1d97f7-4941-4e1a-a17e-ab63945254a4.jpg', N'00fd0654-05dc-4867-9c0f-9c4e9f1f6ebc.jpg')
INSERT [dbo].[Stadium] ([Id], [Name], [Description], [Capacity], [Location], [PicUrl], [BackPicUrl]) VALUES (8, N'Etihad Stadium', N'The Etihad Stadium, located in Manchester, England, is the home ground of Manchester City Football Club. Renowned for its modern design and vibrant atmosphere, it provides a fitting stage for City''s pursuit of footballing excellence, hosting thrilling matches and memorable moments for fans around the world.', 86000, N'Manchester, UK', N'a2bcf494-fae9-4894-b1c4-95c52d50513c.jpg', N'54e63141-8425-4a37-9639-cc778f04e27f.jpg')
INSERT [dbo].[Stadium] ([Id], [Name], [Description], [Capacity], [Location], [PicUrl], [BackPicUrl]) VALUES (9, N'Wembley', N'Wembley Stadium, situated in London, England, is an iconic venue known as the "Home of English Football." With its illustrious history and capacity to host major events, including FA Cup finals and international matches, Wembley stands as a symbol of footballing tradition and excellence, captivating fans with its grandeur and legacy.', 94000, N'London, UK', N'd7ec3478-22bb-43aa-800a-accff6092bf6.jpg', N'3266518d-b6b5-4a6d-ac74-4de4b5acd39e.jpg')
INSERT [dbo].[Stadium] ([Id], [Name], [Description], [Capacity], [Location], [PicUrl], [BackPicUrl]) VALUES (10, N'Signal Iduna Park', N'Signal Iduna Park, located in Dortmund, Germany, is the revered home stadium of Borussia Dortmund. Famous for its electrifying atmosphere generated by the club''s passionate supporters, the stadium, formerly known as Westfalenstadion, stands as a fortress where Dortmund showcases its thrilling brand of football, creating unforgettable moments for fans and players alike.', 88000, N'Dortmund, Germany', N'9ed4ecba-527f-48e4-896e-f758ac95d69a.jpg', N'643351d3-a33d-4c54-a250-79399ceb85d5.jpg')
SET IDENTITY_INSERT [dbo].[Stadium] OFF
GO
SET IDENTITY_INSERT [dbo].[Captain] ON 

INSERT [dbo].[Captain] ([Id], [Name]) VALUES (1, N'Modric')
INSERT [dbo].[Captain] ([Id], [Name]) VALUES (2, N'Ter Stegen')
INSERT [dbo].[Captain] ([Id], [Name]) VALUES (4, N'Virgil Van Dijk')
INSERT [dbo].[Captain] ([Id], [Name]) VALUES (5, N'Manuel Neuer')
INSERT [dbo].[Captain] ([Id], [Name]) VALUES (6, N'Bruno Fernandes')
INSERT [dbo].[Captain] ([Id], [Name]) VALUES (7, N'Marquinhos')
INSERT [dbo].[Captain] ([Id], [Name]) VALUES (8, N'De Bruyne')
INSERT [dbo].[Captain] ([Id], [Name]) VALUES (9, N'Marco Reus')
SET IDENTITY_INSERT [dbo].[Captain] OFF
GO
SET IDENTITY_INSERT [dbo].[Manager] ON 

INSERT [dbo].[Manager] ([Id], [Name]) VALUES (1, N'Carlo Ancelotti')
INSERT [dbo].[Manager] ([Id], [Name]) VALUES (2, N'Xavi')
INSERT [dbo].[Manager] ([Id], [Name]) VALUES (3, N'Jurgen Klopp')
INSERT [dbo].[Manager] ([Id], [Name]) VALUES (4, N'Tuchel')
INSERT [dbo].[Manager] ([Id], [Name]) VALUES (5, N'Erik Ten Hag')
INSERT [dbo].[Manager] ([Id], [Name]) VALUES (6, N'Enrique')
INSERT [dbo].[Manager] ([Id], [Name]) VALUES (7, N'Pep Guardiola')
INSERT [dbo].[Manager] ([Id], [Name]) VALUES (8, N'Edin Terzić')
SET IDENTITY_INSERT [dbo].[Manager] OFF
GO
SET IDENTITY_INSERT [dbo].[Teams] ON 

INSERT [dbo].[Teams] ([Id], [Name], [Description], [LogoUrl], [ManagerId], [CaptainId], [StadiumId]) VALUES (1, N'Real Madrid', N'Real Madrid is one of the most successful football clubs globally, based in Madrid, Spain. Renowned for its historic rivalry with Barcelona, it boasts a record number of UEFA Champions League titles and a rich history of legendary players.', N'efe46ec7-90b9-4ccf-9b7b-c14dad78b568.png', 1, 1, 1)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [LogoUrl], [ManagerId], [CaptainId], [StadiumId]) VALUES (2, N'FC Barcelona', N'Barcelona, based in Catalonia, Spain, is a powerhouse in world football. Known for its unique style of play, "tiki-taka," it has a fierce rivalry with Real Madrid. The club has won numerous domestic and international titles, including multiple UEFA Champions League trophies, with iconic players like Lionel Messi leaving a lasting legacy.', N'bf75d303-084d-493d-b8ad-32cf052513d9.jpg', 2, 2, 2)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [LogoUrl], [ManagerId], [CaptainId], [StadiumId]) VALUES (5, N'Liverpool Fc', N'Liverpool Football Club, based in Liverpool, England, is a storied club with a passionate fanbase. Renowned for its "You''ll Never Walk Alone" anthem, Liverpool has a rich history of success, including multiple European Cup/Champions League titles and numerous domestic league championships.', N'c26bfac7-7c85-4c94-8e56-56cf483ddedd.jpg', 3, 4, 4)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [LogoUrl], [ManagerId], [CaptainId], [StadiumId]) VALUES (6, N'Bayern Munich', N' FC Bayern Munich is a powerhouse in German and European football, based in Munich, Germany. With a rich history of success, the club has amassed numerous domestic league titles and UEFA Champions League trophies. Known for its commitment to excellence both on and off the pitch, Bayern Munich consistently showcases top-tier talent and a winning mentality, solidifying its status as one of the elite football clubs worldwide.', N'5c5c8899-b79c-4de1-aa9f-777b9b81651b.png', 4, 5, 5)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [LogoUrl], [ManagerId], [CaptainId], [StadiumId]) VALUES (7, N'Manchester United', N' Manchester United, based in Manchester, England, is one of the most successful football clubs globally, boasting a rich history of domestic and international triumphs. Known for its passionate fanbase, iconic players, and historic rivalries, it''s a powerhouse in the world of football.', N'3003a11e-6e52-4291-9a15-30af15dae740.jpg', 5, 6, 6)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [LogoUrl], [ManagerId], [CaptainId], [StadiumId]) VALUES (8, N'Paris Saint Germain', N'Paris Saint-Germain (PSG) is a leading football club based in Paris, France, known for its dominance in French football and its ambitious pursuit of European glory. With a star-studded squad and substantial financial backing, PSG consistently competes at the highest level, captivating fans worldwide with its style of play and pursuit of excellence.', N'7c643acc-cff1-4c63-b536-d6b34a64f647.jpg', 6, 7, 7)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [LogoUrl], [ManagerId], [CaptainId], [StadiumId]) VALUES (9, N'Manchester City', N'Manchester City Football Club, based in Manchester, England, has risen to prominence in recent years, known for its attractive style of play and significant investments. With a state-of-the-art stadium and a talented squad, City competes at the forefront of English and European football, aiming for continued success on both domestic and international stages.', N'a5a6c084-587c-41e3-a8a1-98baab7114f8.jpg', 7, 8, 8)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [LogoUrl], [ManagerId], [CaptainId], [StadiumId]) VALUES (15, N'Dortmund', N'Borussia Dortmund, based in Dortmund, Germany, is renowned for its passionate fanbase, vibrant atmosphere at Signal Iduna Park, and exciting style of play. With a rich history of success in German football and a commitment to developing young talent, Dortmund remains a formidable force domestically and in European competitions.', N'd11fe050-76a8-44cb-8a82-42f2f2ebc01f.jpg', 8, 9, 10)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [LogoUrl], [ManagerId], [CaptainId], [StadiumId]) VALUES (16, N'Team', N'hahahah', N'e806e650-be4d-4634-9da1-5afd8684a1f9.jpg', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Teams] OFF
GO
SET IDENTITY_INSERT [dbo].[Match] ON 

INSERT [dbo].[Match] ([Id], [HomeTeamId], [AwayTeamId], [MatchDate], [StadiumId], [CategoryId], [MatchUrl]) VALUES (1, 1, 2, CAST(N'2024-05-06T18:10:00.0000000' AS DateTime2), 1, 14, N'950c6de2-f14a-45cf-bc1a-1d6d14d3ab6d.jpg')
INSERT [dbo].[Match] ([Id], [HomeTeamId], [AwayTeamId], [MatchDate], [StadiumId], [CategoryId], [MatchUrl]) VALUES (2, 5, 7, CAST(N'2024-05-09T12:30:00.0000000' AS DateTime2), 4, 16, N'cf8bfe31-d473-471d-8f16-2b1cd702c856.jpg')
INSERT [dbo].[Match] ([Id], [HomeTeamId], [AwayTeamId], [MatchDate], [StadiumId], [CategoryId], [MatchUrl]) VALUES (3, 6, 5, CAST(N'2024-05-10T22:00:00.0000000' AS DateTime2), 9, 5, N'95741495-0fce-4f28-8bee-948eb35dadc3.jpg')
INSERT [dbo].[Match] ([Id], [HomeTeamId], [AwayTeamId], [MatchDate], [StadiumId], [CategoryId], [MatchUrl]) VALUES (4, 7, 2, CAST(N'2024-05-30T22:30:00.0000000' AS DateTime2), 6, 6, N'7ffe9aa2-336d-4686-aacd-e596ad111422.jpg')
INSERT [dbo].[Match] ([Id], [HomeTeamId], [AwayTeamId], [MatchDate], [StadiumId], [CategoryId], [MatchUrl]) VALUES (5, 9, 8, CAST(N'2024-05-20T22:00:00.0000000' AS DateTime2), 8, 1, N'd8e963f7-bffa-4d74-ab55-615c030660da.jpg')
INSERT [dbo].[Match] ([Id], [HomeTeamId], [AwayTeamId], [MatchDate], [StadiumId], [CategoryId], [MatchUrl]) VALUES (6, 2, 5, CAST(N'2024-06-10T22:00:00.0000000' AS DateTime2), 2, 1, N'19dc2543-4034-4f42-8a1e-b3f76ecd64fc.jpg')
INSERT [dbo].[Match] ([Id], [HomeTeamId], [AwayTeamId], [MatchDate], [StadiumId], [CategoryId], [MatchUrl]) VALUES (8, 8, 15, CAST(N'2024-05-07T22:00:00.0000000' AS DateTime2), 7, 1, N'b10a3def-ed95-4fa1-a7f3-b0ac324f9a4f.jpg')
SET IDENTITY_INSERT [dbo].[Match] OFF
GO
SET IDENTITY_INSERT [dbo].[Ticket] ON 

INSERT [dbo].[Ticket] ([Id], [MatchId], [TicketStatus], [Quantity], [Price], [Section], [QRCode]) VALUES (1, 1, N'Available', 1, CAST(500.00 AS Decimal(10, 2)), N'Category 2 - Gold', NULL)
INSERT [dbo].[Ticket] ([Id], [MatchId], [TicketStatus], [Quantity], [Price], [Section], [QRCode]) VALUES (2, 1, N'Available', 4, CAST(200.00 AS Decimal(10, 2)), N'Category 1 - Normal', NULL)
INSERT [dbo].[Ticket] ([Id], [MatchId], [TicketStatus], [Quantity], [Price], [Section], [QRCode]) VALUES (3, 1, N'Available', 11, CAST(300.00 AS Decimal(10, 2)), N'Category 2 - Normal', NULL)
INSERT [dbo].[Ticket] ([Id], [MatchId], [TicketStatus], [Quantity], [Price], [Section], [QRCode]) VALUES (4, 1, N'Available', 11, CAST(800.00 AS Decimal(10, 2)), N'Category 1 - Premium', NULL)
INSERT [dbo].[Ticket] ([Id], [MatchId], [TicketStatus], [Quantity], [Price], [Section], [QRCode]) VALUES (5, 1, N'Available', 5, CAST(1000.00 AS Decimal(10, 2)), N'Category 1 - VVIP', NULL)
SET IDENTITY_INSERT [dbo].[Ticket] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 

INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (1, 1, CAST(N'2024-05-08T21:00:19.0211245' AS DateTime2), CAST(1500.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (2, 5, CAST(N'2024-05-09T01:58:09.1337881' AS DateTime2), CAST(1000.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (3, 2, CAST(N'2024-05-09T22:10:29.1345161' AS DateTime2), CAST(200.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (4, 4, CAST(N'2024-05-09T22:14:15.1904948' AS DateTime2), CAST(800.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (5, 2, CAST(N'2024-05-09T22:28:01.9175658' AS DateTime2), CAST(200.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (6, 2, CAST(N'2024-05-09T22:29:29.8881951' AS DateTime2), CAST(200.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (7, 4, CAST(N'2024-05-09T22:42:19.2139921' AS DateTime2), CAST(800.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (8, 3, CAST(N'2024-05-09T22:46:56.3982750' AS DateTime2), CAST(300.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (9, 3, CAST(N'2024-05-09T22:51:47.4752575' AS DateTime2), CAST(300.00 AS Decimal(10, 2)), N'a9b1632f-c1ee-4613-88af-d0fdcdff4d32')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (10, 3, CAST(N'2024-05-09T23:49:57.4896647' AS DateTime2), CAST(300.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (11, 3, CAST(N'2024-05-09T23:50:45.6603125' AS DateTime2), CAST(300.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (12, 1, CAST(N'2024-05-10T00:15:00.5799939' AS DateTime2), CAST(500.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (13, 1, CAST(N'2024-05-10T02:53:10.9244704' AS DateTime2), CAST(500.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (14, 1, CAST(N'2024-05-10T04:14:45.1458549' AS DateTime2), CAST(500.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (15, 4, CAST(N'2024-05-10T10:18:24.6886307' AS DateTime2), CAST(800.00 AS Decimal(10, 2)), N'22c24bde-c5ca-4dd0-bda8-19cd893137ef')
INSERT [dbo].[Transaction] ([Id], [TicketId], [DateTime], [TotalPrice], [UserId]) VALUES (1013, 2, CAST(N'2024-10-09T22:05:22.8079244' AS DateTime2), CAST(600.00 AS Decimal(10, 2)), N'2583c9fa-583d-48af-bc6a-c234b8a56d22')
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240429175934_init', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240429193852_init2', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240430112758_update-stadium', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240503232021_updateTeams', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240503233000_updateTeams2', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240504205905_ticket-match', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240504210640_match-url', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240506223532_stadium-pic2', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240507122544_ticket-transaction', N'8.0.4')
GO

5- in powershell run : dotnet user-secrets set "Authentication:Google:ClientSecret" "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"
then run this : dotnet user-secrets set "Authentication:Google:Client" "162912687430-fcqpbo64huck0mqe7h353le881n184mb.apps.googleusercontent.com
