CREATE SCHEMA caproj0

GO

CREATE TABLE caproj0.Manager
(
	ManagerID		INT				NOT NULL		IDENTITY		PRIMARY KEY,
	ManagerPW		NVARCHAR(20)	NOT NULL
)



CREATE TABLE caproj0.StoreLocation
(
	LocationID		INT				NOT NULL		IDENTITY		PRIMARY KEY,
	StoreName		NVARCHAR(40)	NOT NULL,
	Phone			NVARCHAR(40)	NOT NULL,
	Manager			INT				NOT NULL		FOREIGN KEY REFERENCES			caproj0.Manager (ManagerID)
	CONSTRAINT unique_Phone UNIQUE (Phone)
)

--DROP TABLE caproj0.StoreLocation

Create TABLE caproj0.Product
(
	ProductID		INT				NOT NULL		IDENTITY		PRIMARY KEY,
	PName			NVARCHAR(15)	NOT NULL,
	SalesName		NVARCHAR(40),
	Cost			MONEY,
	SalesPrice		AS (Cost * 2.00) -- auto calculate price
)

--DROP TABLE caproj0.Product

CREATE TABLE caproj0.Inventory
(
	LocationID			INT				NOT NULL		FOREIGN KEY REFERENCES			caproj0.StoreLocation (LocationID),
	ProductID			INT				NOT NULL		FOREIGN KEY REFERENCES			caproj0.Product (ProductID),
	Quantity			INT				NOT NULL
	CONSTRAINT PK_LocProdID PRIMARY KEY (LocationID,ProductID)
)

--Composite Key for Inventory -- integrated into table definition
--ALTER TABLE caproj0.Inventory
--    ADD CONSTRAINT PK_LocProdID PRIMARY KEY (LocationID,ProductID)
--GO

--DROP TABLE caproj0.Inventory


CREATE TABLE caproj0.Customer
(
	CustomerID			INT				NOT NULL		IDENTITY		PRIMARY KEY,
	Phone				NVARCHAR(32)	NOT NULL		UNIQUE, --phone is labeled as unique
	FName				NVARCHAR(32)	NOT NULL,
	LName				NVARCHAR(32)	NOT NULL,
	CustomerPW			NVARCHAR(32)	NOT NULL,
	CONSTRAINT unique_CustPhone UNIQUE(Phone)  --ID the constraint.
)

--To drop the customer table
--Drop LineItem
--DROP CustOrders
--then drop customer
--

--DROP TABLE caproj0.Customer


CREATE TABLE caproj0.CustOrder
(
	OrderID				BIGINT			NOT NULL		IDENTITY						PRIMARY KEY,
	CustomerID			INT				NOT NULL		FOREIGN KEY REFERENCES			caproj0.Customer (CustomerID),
	LocationID			INT				NOT NULL		FOREIGN KEY REFERENCES			caproj0.StoreLocation (LocationID),
	OrderDate			DATETIME		NOT NULL		

)


--DROP TABLE caproj0.CustOrder

CREATE TABLE caproj0.LineItem
(
	LineItemID			BIGINT			NOT NULL	IDENTITY					PRIMARY KEY,
	OrderID				BIGINT			NOT NULL FOREIGN KEY REFERENCES			caproj0.CustOrder (OrderID),
	ProductID			INT				NOT NULL FOREIGN KEY REFERENCES			caproj0.Product (ProductID),
	Quantity			INT
	
)

--Composite Key for Line-Item
--ALTER TABLE caproj0.LineItem
--    ADD CONSTRAINT PK_OrdProdID PRIMARY KEY (OrderID,ProductID)
--GO


DROP TABLE caproj0.LineItem

----------------------------------------------------------
--SAMPLE DATA---------------------------------------------
----------------------------------------------------------

--Add some sample data.
-- add manager, then store.
-- if manager does not exist, create manager, then create store
-- else, create store.

INSERT INTO caproj0.Manager ( ManagerPW )
VALUES	('morecowbell'),
		('oldyeller'),
		('PiEqualsCOSEC(-2)')

--CONFIRM
--SELECT * FROM caproj0.Manager


INSERT INTO caproj0.StoreLocation (StoreName, Phone, Manager)
VALUES	(
			'Sams Robotics Surpluss',
			'5551234567',
			(SELECT ManagerID FROM caproj0.Manager WHERE ManagerPW = 'morecowbell')
		),
		(
			'Sallys Cybernetics Salon',
			'3511234567',
			(SELECT ManagerID FROM caproj0.Manager WHERE ManagerPW = 'oldyeller')
		),
		(
			'Big Bills Bots',
			'8168175802',
			(SELECT ManagerID FROM caproj0.Manager WHERE ManagerPW = 'PiEqualsCOSEC(-2)')
		)
	
--Test
--SELECT * FROM caproj0.StoreLocation
 


--make some sample products
	--ProductID		 PName			 SalesName	 Cost	 SalesPrice	
INSERT INTO caproj0.Product ( PName	,		 SalesName,	 Cost	)
VALUES

	('none', 'no desc', 0.00 ),
	('volcanorover', 'Gildas Corrosive-Resistant Casings', 15.00),
	('bipedcasing', 'Leos Living Metal Casings', 13.00),
	('controlCirc', 'Raspberry Tart Control Board', 3.00 ),
	('gears', 'Gear-O-Matic Gears', 15.00 ),
	('resiscapkit', 'Rhondas Resistors and Capacitors Kit', 17.00 ),
	('apendages', 'Autumn-Brand Autonomous Limbs', 25.00 ),
	('opticalsens', 'Omega Optics Optical Enhansement', 50.00 )

--Test
--SELECT * FROM caproj0.Product


INSERT INTO caproj0.Inventory ( LocationID, ProductID, Quantity )
VALUES
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '5551234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'volcanorover'),
			5
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '5551234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'bipedcasing'),
			30
	),

		( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '5551234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'controlCirc'),
			50
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '5551234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'gears'),
			40
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '5551234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'resiscapkit'),
			200
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '5551234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'apendages'),
			40
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '5551234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'opticalsens'),
			50
	),

--2nd store
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '3511234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'volcanorover'),
			3
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '3511234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'bipedcasing'),
			32
	),

		( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '3511234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'controlCirc'),
			55
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '3511234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'gears'),
			38
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '3511234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'resiscapkit'),
			180
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '3511234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'apendages'),
			42
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '3511234567'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'opticalsens'),
			45
	),

--Store 3
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '8168175802'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'volcanorover'),
			3
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '8168175802'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'bipedcasing'),
			32
	),

		( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '8168175802'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'controlCirc'),
			55
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '8168175802'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'gears'),
			38
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '8168175802'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'resiscapkit'),
			180
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '8168175802'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'apendages'),
			42
	),
	( 
		(	SELECT LocationID FROM caproj0.StoreLocation WHERE caproj0.StoreLocation.Phone = '8168175802'),
		(	SELECT ProductID FROM caproj0.Product WHERE caproj0.Product.PName = 'opticalsens'),
			45
	)

--Test
--SELECT * FROM caproj0.Inventory

--get the inventory from a specific store
SELECT ProductID, Quantity FROM caproj0.StoreLocation
JOIN 
caproj0.Inventory ON ( caproj0.StoreLocation.LocationID = caproj0.Inventory.LocationID) AND StoreLocation.Phone = '5551234567';



INSERT INTO caproj0.Customer ( Phone, FName, LName, CustomerPW )
VALUES	('5551234567', 'Lucus', 'Fox', '12345'  ),
		('3331234567', 'Richard', 'Grayson', '54321' ),
		('7892105577', 'Jim', 'Gordon' , '11111' )

--TEST

--raw visible fields.
SELECT FName AS FirstName, LName AS LastName, Phone AS CustPhone FROM caproj0.Customer

--formatted name + phone number
SELECT FName + ' ' + LName AS CustName, Phone AS CustPhone FROM caproj0.Customer

		
--Some sample customer orders
	--OrderID				BIGINT			NOT NULL		IDENTITY		PRIMARY KEY,
	--CustomerID			INT				NOT NULL		FOREIGN KEY REFERENCES			caproj0.Customer (CustomerID),
	--LocationID

	--reference data here
	/*
				'Sams Robotics Surpluss',
			'5551234567',
			(SELECT ManagerID FROM caproj0.Manager WHERE ManagerPW = 'morecowbell')
		),
		(
			'Sallys Cybernetics Salon',
			'3511234567',
			(SELECT ManagerID FROM caproj0.Manager WHERE ManagerPW = 'oldyeller')
		),
		(
			'Big Bills Bots',
			'8168175802',
			(SELECT ManagerID FROM caproj0.Manager WHERE ManagerPW = 'PiEqualsCOSEC(-2)')
		)
	
	*/

--> ::add a date and time:: <--

INSERT INTO caproj0.CustOrder ( CustomerID, LocationID, OrderDate )
VALUES
		( ( SELECT CustomerID FROM caproj0.Customer WHERE Phone = '5551234567') , ( SELECT LocationID FROM caproj0.StoreLocation WHERE Phone = '5551234567' ), '2019-10-14 00:00:00.000'),
		( ( SELECT CustomerID FROM caproj0.Customer WHERE Phone = '3331234567') , ( SELECT LocationID FROM caproj0.StoreLocation WHERE Phone = '3511234567' ), '2019-10-14 00:00:01.000'),
		( ( SELECT CustomerID FROM caproj0.Customer WHERE Phone = '7892105577') , ( SELECT LocationID FROM caproj0.StoreLocation WHERE Phone = '8168175802' ), '2019-10-14 00:00:02.000')

--test
SELECT * FROM caproj0.CustOrder

--more readable output
SELECT OrderID AS OrderNumber , LocationID AS LocationNum, CONVERT(varchar, OrderDate, 9) AS OrderTimeStamp FROM caproj0.CustOrder

--Line items inserted into an order.
INSERT INTO caproj0.LineItem ( OrderID, ProductID, Quantity )
VALUES
		--lucus fox's order
		(  ( SELECT OrderID FROM caproj0.CustOrder WHERE CustOrder.CustomerID = (SELECT CustomerID FROM caproj0.Customer WHERE Customer.Phone = '5551234567')  AND CustOrder.LocationID = (SELECT LocationID FROM caproj0.StoreLocation WHERE StoreLocation.Phone = '5551234567')  AND CustOrder.OrderDate = '2019-10-14 00:00:00.000'), (SELECT ProductID FROM caproj0.Product WHERE PName = 'none' ), 4),
		(  ( SELECT OrderID FROM caproj0.CustOrder WHERE CustOrder.CustomerID = (SELECT CustomerID FROM caproj0.Customer WHERE Customer.Phone = '5551234567')  AND CustOrder.LocationID = (SELECT LocationID FROM caproj0.StoreLocation WHERE StoreLocation.Phone = '5551234567')  AND CustOrder.OrderDate = '2019-10-14 00:00:00.000'), (SELECT ProductID FROM caproj0.Product WHERE PName = 'none' ), 2),
		(  ( SELECT OrderID FROM caproj0.CustOrder WHERE CustOrder.CustomerID = (SELECT CustomerID FROM caproj0.Customer WHERE Customer.Phone = '5551234567')  AND CustOrder.LocationID = (SELECT LocationID FROM caproj0.StoreLocation WHERE StoreLocation.Phone = '5551234567')  AND CustOrder.OrderDate = '2019-10-14 00:00:00.000'), (SELECT ProductID FROM caproj0.Product WHERE PName = 'none' ), 5),

		--richard grayson's order
		(  ( SELECT OrderID FROM caproj0.CustOrder WHERE CustOrder.CustomerID = (SELECT CustomerID FROM caproj0.Customer WHERE Customer.Phone = '3331234567')  AND CustOrder.LocationID = (SELECT LocationID FROM caproj0.StoreLocation WHERE StoreLocation.Phone = '3511234567')  AND CustOrder.OrderDate = '2019-10-14 00:00:01.000'), (SELECT ProductID FROM caproj0.Product WHERE PName = 'controlCirc' ), 2),
		(  ( SELECT OrderID FROM caproj0.CustOrder WHERE CustOrder.CustomerID = (SELECT CustomerID FROM caproj0.Customer WHERE Customer.Phone = '3331234567')  AND CustOrder.LocationID = (SELECT LocationID FROM caproj0.StoreLocation WHERE StoreLocation.Phone = '3511234567')  AND CustOrder.OrderDate = '2019-10-14 00:00:01.000'), (SELECT ProductID FROM caproj0.Product WHERE PName = 'gears' ), 2),

		--jim gordon's order
		(  ( SELECT OrderID FROM caproj0.CustOrder WHERE CustOrder.CustomerID = (SELECT CustomerID FROM caproj0.Customer WHERE Customer.Phone = '7892105577')  AND CustOrder.LocationID = (SELECT LocationID FROM caproj0.StoreLocation WHERE StoreLocation.Phone = '8168175802')  AND CustOrder.OrderDate = '2019-10-14 00:00:02.000'), (SELECT ProductID FROM caproj0.Product WHERE PName = 'opticalsens' ), 1)

--TEST
--Get all
SELECT * FROM caproj0.LineItem

--Get by specific location.
SELECT * FROM caproj0.LineItem WHERE OrderID = (SELECT OrderID FROM caproj0.CustOrder WHERE CustOrder.LocationID = (SELECT LocationID From caproj0.StoreLocation WHERE Phone = '3511234567'));

--Get by specific customer
SELECT * FROM caproj0.LineItem WHERE OrderID = (SELECT OrderID FROM caproj0.CustOrder WHERE CustOrder.CustomerID = (SELECT CustomerID From caproj0.Customer WHERE Phone = '3331234567'));
--8168175802
--Join with orders  to show Which Order, Who bought it, where they baught it, when they baught it, What they baught,
SELECT * FROM caproj0.CustOrder
JOIN
caproj0.LineItem ON ( caproj0.CustOrder.OrderID = caproj0.LineItem.OrderID);