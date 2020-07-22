create table UserRole(
id int primary key identity(1,1),
Name nvarchar(20) not null,
Note nvarchar(50)
)
create table UserInfo
(
UserID int primary key identity(0001,1),
UserRole int foreign key references UserRole(id),
UserName nvarchar(50) not null,
UserPwd nvarchar(50) not null,
NickName nvarchar(50),
Email nvarchar(50),
IsVip int,
Images nvarchar(50),
Integral int,
Money money,
RegistrationTime date,
Note nvarchar(50)
)
create table ProductType(
ProductTypeID int primary key identity(1,1),
ProductTypeName nvarchar(50) not null,
Images nvarchar(50)  null,
Sort int  null unique,
ErciID  int foreign key references ProductType(ProductTypeID),
Note nvarchar(50) null
)
create table Products(
ProductID int primary key identity(1000,1),
ProductTypeID  int foreign key references ProductType(ProductTypeID),
ProductName nvarchar(50) not null,
ProDescription nvarchar(100) not null,
ShortPrice money not null,
Price money not null,
ProQuantity int not null,
SallQuantity int not null ,
ProImage nvarchar(50) not null,
UpdateTime date,
Note nvarchar(50)
)
create table Integrals(
IntegralID int primary key identity(1,1),
ProductID int foreign key references Products(ProductID),
IntegralValue int not null,
Note nvarchar(50)
)
create table ProductSku(
SkuID int primary key identity(1,1),
ProductID int foreign key references Products(ProductID),
SkuName nvarchar(50) not null,
Skuquantity int not null,
SkuPrice money,
Note nvarchar(50)
)
create table Comments(
CommentsID int primary key identity(1,1),
UserID int foreign key references UserInfo(UserID),
ProductID int foreign key references Products(ProductID),
TextContent nvarchar(100),
Images nvarchar(50),
Stars int not null,
Time date,
IsAudit int default(0),
Note nvarchar(50)
)
create table ShopCar(
CarID int primary key identity(1,1),
ProductID int foreign key references Products(ProductID),
UserID int foreign key references UserInfo(UserID),
Quantity int not null,
CreateTime date ,
SkuID int foreign key references ProductSku(SkuID),
Note nvarchar(50)
)
create table OrderInfo(
OrderID int primary key,
UserID int foreign key references UserInfo(UserID),
OrPrice money not null,
ContacName nvarchar(50) not null,
ContacMoblie int not null,
ContacAddress nvarchar(50) not null,
OrderStatus int  default(0),
CreateTime date,
Note nvarchar(50) null
)
create table Order_Product(
OPID int primary key identity(1,1),
OrderID int foreign key references OrderInfo(OrderID),
ProductID int foreign key references Products(ProductID),
Note nvarchar(50)
)
create table Logistics(
LogisticsID int primary key identity(1,1),
OrderID int foreign key references OrderInfo(OrderID),
LogisticsNum nvarchar(20) not null,
ConpanyName nvarchar(20) not null,
ContactName nvarchar(50) not null,
ContactMoblie int not null,
ContactAddress nvarchar(50) not null,
ContactNote  nvarchar(50) not null,
LogisticsStatus int default(0),
CreateTime date default(getDate()),
Note nvarchar(50) null
)
create table Article_Category(
ACID int primary key identity(1,1),
ACName nvarchar(50) not null,
UserID int foreign key references UserInfo(UserID),
UpdateTime date default(getDate()),
Note nvarchar(50) null
)
create table Article(
ArticleID int primary key identity(1,1),
Articletitle nvarchar(50) not null,
ArticleContent  nvarchar(50) not null,
UpdateTime date default(getDate()),
ACID int foreign key references Article_Category(ACID),
UserID int foreign key references UserInfo(UserID),
Note nvarchar(50) null
)
create table Dynamics(
DynamicID  int primary key identity(1,1),
UserID int foreign key references UserInfo(UserID),
DynamicContent nvarchar(200) not null,
IsAudit int default(0),
CreateTime date default(getDate()),
Note nvarchar(50) null
)
create table DynamicComment(
DCID int primary key identity(1,1),
UserID int foreign key references UserInfo(UserID),
DynamicID int foreign key references Dynamics(DynamicID),
Content  nvarchar(50) not null,
Times date default(getDate()),
IsAudit int default(0),
Note nvarchar(50) null
)
create table Moneys(
MoneyID  int primary key identity(1,1),
Money money not null,
Note nvarchar(50) null
)
create table Contect(
ContectID int primary key identity(1,1),
UserID int foreign key references UserInfo(UserID),
ContencFlag int ,
ContactName nvarchar(50) not null,
ContactMoblie int not null,
ContactAddress nvarchar(50) not null,
Note nvarchar(50) null
)
create table Wishlist(
WishlistID  int primary key identity(1,1),
ProductID int foreign key references Products(ProductID),
UserID int foreign key references UserInfo(UserID),
SkuID int foreign key references ProductSku(SkuID),
WishTime date default(getDate()),
Note nvarchar(50) null
)
create table Footprint(
FootprintID  int primary key identity(1,1),
ProductID int foreign key references Products(ProductID),
UserID int foreign key references UserInfo(UserID),
CreateTime date default(getDate()),
Note nvarchar(50) null
)
create table Advert(
AdvertID  int primary key identity(1,1),
UserID int foreign key references UserInfo(UserID),
Stor int null,
Url nvarchar(100) null,
Images nvarchar(50) not null,
CreateTime datetime ,
Note nvarchar(50) null
)
create table Costs(
CostsID  int primary key identity(1,1),
UserID int foreign key references UserInfo(UserID),
CostMoney money not null,
CostType nvarchar(50) not null,
CreateTime date default(getDate()),
Note nvarchar(50) null
)
