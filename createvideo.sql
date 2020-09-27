create database videoAdo
go
use videoAdo
go


CREATE TABLE [Genres](
	[GenreNr] [int] IDENTITY(1,1) NOT NULL,
	[Genre] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[GenreNr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [Films](
	[BandNr] [int] IDENTITY(1,1) NOT NULL,
	[Titel] [nchar](50) NOT NULL,
	[GenreNr] [int] NOT NULL constraint fk_GenreNr foreign key references Genres(GenreNr),
	[InVoorraad] [int] NOT NULL,
	[UitVoorraad] [int] NOT NULL,
	[Prijs] [money] NOT NULL,
	[TotaalVerhuurd] [int] NOT NULL,
 CONSTRAINT [PK_Films] PRIMARY KEY CLUSTERED 
(
	[BandNr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET IDENTITY_INSERT [Genres] ON
GO

--
-- Dumping data for table 'Genres'
--

INSERT  [Genres] ([GenreNr], [Genre]) VALUES (1, N'AKTIE')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (2, N'AVONTUUR')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (3, N'WESTERN')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (4, N'EROTIEK')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (5, N'HORROR')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (6, N'HUMOR')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (7, N'KINDERFILM')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (8, N'OORLOG')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (9, N'PIRATENFILM')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (10, N'SCIENCE FICTION')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (11, N'SENTIMENTEEL')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (12, N'FANTASY')
INSERT  [Genres] ([GenreNr], [Genre]) VALUES (13, N'THRILLER')
-- 13 records

SET IDENTITY_INSERT [Genres] OFF
GO

SET IDENTITY_INSERT [Films] ON
GO
--
-- Dumping data for table 'Films'
--

INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (1, N'RAIDERS OF THE LOST ARK', 2, 3, 3, 3, 213)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (2, N'E T', 10, 3, 1, 3.5, 211)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (3, N'LOVE STORY', 11, 1, 1, 4, 234)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (4, N'TWO MOON JUNCTION', 4, 8, 3, 3, 14)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (5, N'POLICE ACADEMY', 6, 3, 2, 2, 346)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (6, N'ONCE UPON A TIME IN THE WEST', 3, 2, 2, 2, 142)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (7, N'TRON', 10, 3, 3, 2.5, 523)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (8, N'DE SNORKELS', 7, 2, 2, 1, 243)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (9, N'ZORRO', 1, 2, 1, 1.5, 387)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (10, N'HECTOR', 6, 2, 2, 1, 23)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (11, N'HIGH NOON', 3, 4, 1, 2, 125)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (12, N'PIRATES OF THE CARRIBEAN', 9, 2, 1, 2.5, 32)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (13, N'SAVING PRIVATE RYAN', 8, 3, 3, 2, 387)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (14, N'THE DEER HUNTER', 1, 9, 3, 1.4, 24)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (15, N'THE GODS MUST BE CRAZY', 6, 6, 6, 2, 22)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (16, N'FRIDAY THE 13TH', 5, 4, 1, 2, 21)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (17, N'THE BIRDS', 13, 4, 2, 2, 285)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (18, N'THE HOBBIT', 12, 4, 1, 3.5, 1)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (19, N'BATMAN', 1, 12, 6, 3, 21)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (20, N'ERAGON', 12, 6, 5, 2, 5)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (21, N'TERMS OF ENDEARMENT', 11, 8, 6, 1, 24)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (22, N'EMANUELLE', 4, 4, 1, 2, 355)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (23, N'KRAMER VS KRAMER', 11, 1, 1, 1.5, 156)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (24, N'STAR WARS', 10, 5, 4, 2, 1)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (25, N'EL GRINGO', 3, 5, 1, 1, 44)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (26, N'THE GRADUATE', 11, 3, 1, 1.5, 346)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (27, N'THE HANGOVER', 6, 6, 2, 2, 12)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (28, N'THE OMEN', 5, 5, 2, 1.5, 411)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (29, N'SEX,LIES AND VIDEOTAPES', 4, 6, 2, 1.5, 12)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (30, N'THE ABYSS', 10, 7, 3, 2, 33)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (31, N'DE SMURFEN', 7, 6, 4, 1, 12)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (32, N'FIRST BLOOD', 1, 3, 2, 2, 200)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (33, N'THE LORD OF THE RINGS', 12, 5, 1, 3, 12)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (34, N'DE LANGSTE DAG', 8, 3, 2, 1.75, 55)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (35, N'THE GUNS OF NAVARONE', 8, 2, 1, 1, 234)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (36, N'CISKE DE RAT', 7, 6, 2, 1.5, 2)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (37, N'THE REVENGE OF JAWS', 2, 6, 3, 2, 11)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (38, N'SHUTTER ISLAND', 13, 3, 1, 2, 3)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (39, N'JURASSIC PARK', 10, 5, 2, 3, 22)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (40, N'THE EXORCIST', 5, 2, 2, 1.5, 123)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (41, N'DOORNROOSJE', 7, 5, 2, 1, 2)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (42, N'TRUE GRITT', 3, 5, 2, 2, 11)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (43, N'PROMETHEUS', 10, 5, 1, 3, 13)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (44, N'SHREK', 7, 4, 2, 2, 4)
INSERT  [Films] ([BandNr], [Titel], [GenreNr], [InVoorraad], [UitVoorraad], [Prijs], [TotaalVerhuurd]) VALUES (45, N'LICENCE TO KILL', 1, 6, 2, 1.5, 2)
-- 45 records

SET IDENTITY_INSERT [Films] OFF
GO
