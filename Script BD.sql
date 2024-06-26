CREATE TABLE [dbo].[Categoria](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Favorito](
	[IdFavorito] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdReceta] [int] NOT NULL,
 CONSTRAINT [PK_Favorito] PRIMARY KEY CLUSTERED 
(
	[IdFavorito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Historial](
	[IdHistorial] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdReceta] [int] NOT NULL,
 CONSTRAINT [PK_Historial] PRIMARY KEY CLUSTERED 
(
	[IdHistorial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Ingrediente](
	[IdIngrediente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[Densidad] [float] NULL,
	[Imagen] [nvarchar](max) NULL,
	[Almacenamiento] [nvarchar](max) NULL,
	[Sustitutivo] [int] NULL,
	[Medible] [bit] NOT NULL,
 CONSTRAINT [PK_Ingrediente] PRIMARY KEY CLUSTERED 
(
	[IdIngrediente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[Ingrediente_Receta](
	[IdIngrediente_Receta] [int] IDENTITY(1,1) NOT NULL,
	[IdReceta] [int] NOT NULL,
	[IdIngrediente] [int] NOT NULL,
	[Cantidad] [float] NOT NULL,
 CONSTRAINT [PK_Ingrediente_Receta] PRIMARY KEY CLUSTERED 
(
	[IdIngrediente_Receta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Receta](
	[IdReceta] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[Instrucciones] [nvarchar](max) NULL,
	[Imagen] [nvarchar](max) NULL,
	[IdCategoria] [int] NULL,
	[TiempoPreparacion] [int] NULL,
	[Visitas] [int] NULL,
	[MediaValoraciones] [float] NULL,
 CONSTRAINT [PK_Receta] PRIMARY KEY CLUSTERED 
(
	[IdReceta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Salt] [nvarchar](55) NOT NULL,
	[Password] [varbinary](max) NOT NULL,
	[IdRol] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[Utensilio](
	[IdUtensilio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[Volumen] [float] NOT NULL,
	[Imagen] [nvarchar](max) NULL,
	[Recomendacion] [nvarchar](max) NULL,
 CONSTRAINT [PK_Utensilio] PRIMARY KEY CLUSTERED 
(
	[IdUtensilio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[Valoraciones](
	[IdValoracion] [int] IDENTITY(1,1) NOT NULL,
	[IdReceta] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Valoracion] [int] NOT NULL,
 CONSTRAINT [PK_Valoraciones] PRIMARY KEY CLUSTERED 
(
	[IdValoracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([IdCategoria], [Nombre], [Descripcion]) VALUES (1, N'Cocina Internacional', N'Descubre los sabores del mundo con nuestra colección de recetas internacionales. Desde platos clásicos hasta delicias exóticas, encontrarás inspiración para llevar tus papilas gustativas a un viaje culinario único.')
INSERT [dbo].[Categoria] ([IdCategoria], [Nombre], [Descripcion]) VALUES (2, N'Saludables y Fitness', N'Mantén un estilo de vida saludable sin sacrificar el sabor con nuestras recetas diseñadas para cuidar de ti. Desde opciones bajas en calorías hasta platos ricos en nutrientes, aquí encontrarás el equilibrio perfecto entre salud y sabor.')
INSERT [dbo].[Categoria] ([IdCategoria], [Nombre], [Descripcion]) VALUES (3, N'Repostería y Dulces', N'Endulza tu día con nuestra selección de recetas de repostería y dulces. Desde pasteles decadentes hasta galletas crujientes, te invitamos a deleitar tu paladar con nuestras deliciosas creaciones.')
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Ingrediente] ON 

INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (1, N'Harina de Trigo', 0.5, NULL, N'En un tupper', NULL, 1)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (6, N'Huevo', NULL, NULL, N'Nevera', NULL, 0)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (7, N'Plátano', NULL, NULL, N'A la interperie', NULL, 0)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (8, N'Tomate', NULL, NULL, N'A la interperie', NULL, 0)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (11, N'Arroz', 1.38, NULL, N'En un tarro', NULL, 1)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (12, N'Zanahoria', NULL, NULL, N'En la nevera', NULL, 0)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (13, N'Guisantes', 1.05, NULL, N'En un tupper', NULL, 1)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (15, N'Pechuga de pollo', NULL, NULL, N'En la nevera', NULL, 0)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (16, N'Almendras', 1, NULL, N'En un tarro', NULL, 1)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (17, N'Cebolla', NULL, NULL, N'A la interperie', NULL, 0)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (18, N'Caldo de pollo', 1, NULL, N'En la nevera', NULL, 1)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (19, N'Salsa de soja', 1, NULL, N'En la nevera', NULL, 1)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (20, N'Pan', NULL, NULL, N'A la interperie', NULL, 0)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (21, N'Leche', 1, NULL, N'En la nevera', NULL, 1)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (22, N'Azucar', 1.59, NULL, N'En un tarro', NULL, 1)
INSERT [dbo].[Ingrediente] ([IdIngrediente], [Nombre], [Densidad], [Imagen], [Almacenamiento], [Sustitutivo], [Medible]) VALUES (23, N'Aceite de oliva', 0.92, NULL, N'En una botella', NULL, 1)
SET IDENTITY_INSERT [dbo].[Ingrediente] OFF
GO
SET IDENTITY_INSERT [dbo].[Ingrediente_Receta] ON 

INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (1, 1, 6, 2)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (2, 1, 7, 1)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (3, 2, 8, 4)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (4, 3, 11, 400)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (5, 3, 12, 1)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (6, 3, 13, 75)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (7, 3, 6, 2)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (8, 4, 15, 3)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (9, 4, 19, 100)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (10, 4, 16, 110)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (11, 4, 17, 1)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (12, 4, 12, 2)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (13, 5, 20, 1)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (14, 5, 21, 1000)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (15, 5, 22, 100)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (16, 5, 23, 1000)
INSERT [dbo].[Ingrediente_Receta] ([IdIngrediente_Receta], [IdReceta], [IdIngrediente], [Cantidad]) VALUES (17, 5, 6, 2)
SET IDENTITY_INSERT [dbo].[Ingrediente_Receta] OFF
GO
SET IDENTITY_INSERT [dbo].[Receta] ON 

INSERT [dbo].[Receta] ([IdReceta], [Nombre], [Instrucciones], [Imagen], [IdCategoria], [TiempoPreparacion], [Visitas], [MediaValoraciones]) VALUES (1, N'Tortitas de plátano y huevo', N'Tritura el plátano y mézclalo con los huevos batidos.
Calienta una sartén a fuego medio y vierte pequeñas porciones de la mezcla.
Cocina hasta que estén doradas por ambos lados. Sirve caliente.', N'tortitasplatanohuevo.jpg', 2, 10, 77, NULL)
INSERT [dbo].[Receta] ([IdReceta], [Nombre], [Instrucciones], [Imagen], [IdCategoria], [TiempoPreparacion], [Visitas], [MediaValoraciones]) VALUES (2, N'Sopa de tomate y albahaca', N'Hierve los tomates en agua durante unos minutos para pelarlos fácilmente.
Tritura los tomates pelados con las hojas de albahaca.
Calienta la sopa y sírvela caliente con un poco de albahaca fresca encima.', N'sopatomatealbahaca.jpg', 1, 20, 30, NULL)
INSERT [dbo].[Receta] ([IdReceta], [Nombre], [Instrucciones], [Imagen], [IdCategoria], [TiempoPreparacion], [Visitas], [MediaValoraciones]) VALUES (3, N'Arroz tres delicias', N'Cortamos la zanahoria en dados pequeños y la ponemos a cocer en una cacerola con agua y un poco de sal. Abrimos la lata de guisantes. Batimos los huevos con la sal y una cucharadita de azúcar y preparamos una tortilla francesa en dos tandas, usando una sartén bien caliente con media cucharada de aceite de oliva. La tortilla debe quedarnos bastante fina, tipo crepe.

Mientras tanto, ponemos en otro cazo con agua de sal el arroz largo tipo basmati o thai a cocer. En unos diez minutos estará listo, dependiendo de la variedad, momento en el que lo escurrimos y reservamos. Mientras cuece, cortamos el jamón de York en taquitos.

Salteamos las gambas ligeramente en una sartén amplia con el resto del aceite de oliva, y como ya tenemos todos los ingredientes listos, procedemos a preparar el plato de arroz tres delicias. Para ello, añadimos el arroz bien escurrido a la sartén, y sazonamos con las cucharadas de salsa de soja.

Una vez bien salteado, agregamos los demás ingredientes, salteando para que todos se mezclen en la sartén y una vez listos lo pasamos a una fuente y lo servimos inmediatamente, muy caliente con un poco de salsa de soja aparte para que quien quiera pueda añadirla a su gusto.', N'arroztresdelicias.jpg', 1, 20, 8, NULL)
INSERT [dbo].[Receta] ([IdReceta], [Nombre], [Instrucciones], [Imagen], [IdCategoria], [TiempoPreparacion], [Visitas], [MediaValoraciones]) VALUES (4, N'Pollo con almendras', N'Limpiamos bien las pechugas de pollo, retirando restos de tendones y grasa, y las cortamos en piezas de bocado. Sumergimos en pollo en una mezcla de salsa de soja, jengibre molido y azúcar moreno o, en su defecto, azúcar blanco. Dejamos reposar en la nevera durante al menos una hora, para que el pollo se impregne bien de la marinada y quede sabroso y jugoso. Mientras tanto preparamos el resto de ingredientes. Calentamos un poco de aceite en una sartén y freímos las almendras a fuego medio-alto hasta que estén doradas. Retiramos y reservamos. En el mismo aceite y sartén salteamos la cebolla y la zanahoria, previamente peladas y cortadas groseramente. Solo queremos que pierdan un poco de dureza, así que con dos o tres minutos será suficiente. Apagamos el fuego y devolvemos las almendras a la sartén. Reservamos hasta que el pollo esté marinado.', N'polloconalmendras.jpg', 1, 45, 0, NULL)
INSERT [dbo].[Receta] ([IdReceta], [Nombre], [Instrucciones], [Imagen], [IdCategoria], [TiempoPreparacion], [Visitas], [MediaValoraciones]) VALUES (5, N'Torrijas de leche', N'Dejamos el pan en remojo durante una hora o hasta que absorba toda la leche y no se vean restos. Batimos los huevos en un recipiente hondo, pasamos las rebanadas de pan por ambas caras y las freímos en abundante aceite de oliva bien caliente, volteando para que se doren por las dos caras. Las escurrimos bien y las ponemos en papel secante para quitar el exceso de aceite.
Mezclamos 100 g de azúcar con dos cucharaditas de canela molida (opcional) y rebozamos las torrijas en la mezcla. Servimos con fruta fresca, espolvoreamos con nueces picadas o tal cual y sin florituras. Como más nos guste. Eso sí, mejor calientes y recién hechas que es su momento óptimo.', N'torrijas.jpg', 3, 60, 0, NULL)
SET IDENTITY_INSERT [dbo].[Receta] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([IdRol], [Rol]) VALUES (1, N'Admin')
INSERT [dbo].[Rol] ([IdRol], [Rol]) VALUES (2, N'Usuario')
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Email], [Salt], [Password], [IdRol]) VALUES (8, N'admin', N'admin@gmail.com', N'%º·±>§~Æ^GÖîe¼B*J:½Û¨ÒFNn<PhÜ''.Õ¢%kQ', 0xFF7DB53B82B19DA224E6DC9E65658782EFCB42C865F9DC72EC64E9ED1724AA52A02CE3A685C2845A10DA05857BAB86183192D8EEEAA30A3F5997075BF2A47104, 2)
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Email], [Salt], [Password], [IdRol]) VALUES (9, N'alejandro', N'alejandro@gmail.com', N'5ùDÇÜ··ktè¦hj]zjÃßÜ\C)>æzh B>¡ßÕ°ì-T¹ðÄ¿4', 0x974A9DA09B3FA31EBE77FFC61CFB52ED8081CB584B7D0A9EBAF06432D07B4D36F0307A9B228AFF9904A7D36BE9E96B0C832C7908957322CC07E916BE170F3AFB, 1)
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Email], [Salt], [Password], [IdRol]) VALUES (10, N'prueba', N'prueba@gmail.com', N'½t)iyþÄæ]\WFò³~îÙ6©7ÞÜîÉz%ªbl,ùòþhõæµ&Á', 0x8EB82BB11E9524EDB04F8A5FC667A624C68EABB09F7BDB65054BF0ECCABBF02B561AA100E8D235CB2A28AD506F5414639AED4A75A0A978C63DCFDADC16B8B3B0, 1)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Utensilio] ON 

INSERT [dbo].[Utensilio] ([IdUtensilio], [Nombre], [Volumen], [Imagen], [Recomendacion]) VALUES (1, N'Cuchara', 5, N'spoon.svg', N'Colmada')
INSERT [dbo].[Utensilio] ([IdUtensilio], [Nombre], [Volumen], [Imagen], [Recomendacion]) VALUES (2, N'Cuchara sopera', 15, N'spoon.svg', N'Colmada')
INSERT [dbo].[Utensilio] ([IdUtensilio], [Nombre], [Volumen], [Imagen], [Recomendacion]) VALUES (3, N'Cucharon', 80, N'ladle.svg', N'Colmada')
INSERT [dbo].[Utensilio] ([IdUtensilio], [Nombre], [Volumen], [Imagen], [Recomendacion]) VALUES (4, N'Vaso', 200, N'glass.svg', N'Hasta arriba')
INSERT [dbo].[Utensilio] ([IdUtensilio], [Nombre], [Volumen], [Imagen], [Recomendacion]) VALUES (5, N'Taza de te', 150, N'mug.svg', N'Hasta arriba')
INSERT [dbo].[Utensilio] ([IdUtensilio], [Nombre], [Volumen], [Imagen], [Recomendacion]) VALUES (7, N'Taza', 100, N'mug.svg', N'Hasta arriba')
INSERT [dbo].[Utensilio] ([IdUtensilio], [Nombre], [Volumen], [Imagen], [Recomendacion]) VALUES (8, N'Tazón', 250, N'bowl.svg', N'Hasta arriba')
SET IDENTITY_INSERT [dbo].[Utensilio] OFF
GO
SET IDENTITY_INSERT [dbo].[Valoraciones] ON 

INSERT [dbo].[Valoraciones] ([IdValoracion], [IdReceta], [IdUsuario], [Valoracion]) VALUES (1, 1, 8, 3)
INSERT [dbo].[Valoraciones] ([IdValoracion], [IdReceta], [IdUsuario], [Valoracion]) VALUES (3, 2, 8, 5)
SET IDENTITY_INSERT [dbo].[Valoraciones] OFF
GO
ALTER TABLE [dbo].[Favorito]  WITH CHECK ADD  CONSTRAINT [FK_Favorito_Receta] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Receta] ([IdReceta])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Favorito] CHECK CONSTRAINT [FK_Favorito_Receta]
GO
ALTER TABLE [dbo].[Favorito]  WITH CHECK ADD  CONSTRAINT [FK_Favorito_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Favorito] CHECK CONSTRAINT [FK_Favorito_Usuario]
GO
ALTER TABLE [dbo].[Historial]  WITH CHECK ADD  CONSTRAINT [FK_Historial_Receta] FOREIGN KEY([IdReceta])
REFERENCES [dbo].[Receta] ([IdReceta])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Historial] CHECK CONSTRAINT [FK_Historial_Receta]
GO
ALTER TABLE [dbo].[Historial]  WITH CHECK ADD  CONSTRAINT [FK_Historial_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Historial] CHECK CONSTRAINT [FK_Historial_Usuario]
GO
ALTER TABLE [dbo].[Ingrediente_Receta]  WITH CHECK ADD  CONSTRAINT [FK_Ingrediente_Receta_Ingrediente] FOREIGN KEY([IdIngrediente])
REFERENCES [dbo].[Ingrediente] ([IdIngrediente])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ingrediente_Receta] CHECK CONSTRAINT [FK_Ingrediente_Receta_Ingrediente]
GO
ALTER TABLE [dbo].[Ingrediente_Receta]  WITH CHECK ADD  CONSTRAINT [FK_Ingrediente_Receta_Receta] FOREIGN KEY([IdReceta])
REFERENCES [dbo].[Receta] ([IdReceta])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ingrediente_Receta] CHECK CONSTRAINT [FK_Ingrediente_Receta_Receta]
GO
ALTER TABLE [dbo].[Receta]  WITH CHECK ADD  CONSTRAINT [FK_Receta_Categoria] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[Receta] CHECK CONSTRAINT [FK_Receta_Categoria]
GO
ALTER TABLE [dbo].[Valoraciones]  WITH CHECK ADD  CONSTRAINT [FK_Valoraciones_Receta] FOREIGN KEY([IdReceta])
REFERENCES [dbo].[Receta] ([IdReceta])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Valoraciones] CHECK CONSTRAINT [FK_Valoraciones_Receta]
GO
ALTER TABLE [dbo].[Valoraciones]  WITH CHECK ADD  CONSTRAINT [FK_Valoraciones_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Valoraciones] CHECK CONSTRAINT [FK_Valoraciones_Usuario]
GO

CREATE PROCEDURE [dbo].[SP_RECETA_VALORACION_INGREDIENTES]
    (@IDRECETA INT, @VALORACION INT OUT)
    AS
	    SELECT @VALORACION = ISNULL(AVG(VALORACION), 1) FROM VALORACIONES WHERE IDRECETA=@IDRECETA

	    SELECT IR.IDINGREDIENTE, I.NOMBRE, IR.CANTIDAD, I.MEDIBLE
        FROM INGREDIENTE_RECETA AS IR
        INNER JOIN INGREDIENTE AS I ON IR.IDINGREDIENTE = I.IDINGREDIENTE
        WHERE IR.IDRECETA = @IDRECETA
GO

CREATE PROCEDURE [dbo].[SP_RECETA_VALORACION_INGREDIENTES_DETALLE]
(@IDRECETA INT, @VALORACION INT OUT)
AS
	SELECT @VALORACION = ISNULL(AVG(VALORACION), 1) FROM VALORACIONES WHERE IDRECETA=@IDRECETA

	SELECT IR.IDINGREDIENTE, I.NOMBRE, IR.CANTIDAD
    FROM INGREDIENTE_RECETA AS IR
    INNER JOIN INGREDIENTE AS I ON IR.IDINGREDIENTE = I.IDINGREDIENTE
    WHERE IR.IDRECETA = @IDRECETA;
GO
