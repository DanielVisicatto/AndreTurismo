DELETE FROM [City];
GO
DELETE FROM [Address];
GO
DELETE FROM [Customer];
GO
DELETE FROM [Hotel];
GO
DELETE FROM [Ticket];
GO
DELETE FROM [Package];

SELECT * FROM [Customer];

             SELECT      c.Name CustomerName
                       , c.PhoneNumber CustomerPhone
                       , c.CellPhoneNumber CustomerCellPhone
                       , c.Id CustomerId 
                       , c.RegisterDate CustomerRegister
                       , c.Id_Address CustomerAddress

                       , a.Id AddressId 
                       , a.Street AddressStreet 
                       , a.Number AddressNumber 
                       , a.Neighborhood AddressNeghb 
                       , a.ZipCode AddressZip 
                       , a.Complement AddressComp 
                       , a.City AddressCity 
                       , a.RegisterDate AddressReg 

                       , ct.Id CityId 
                       , ct.Description CityDesc 
                       , ct.RegisterDate Cityreg 
                   FROM [Customer] c,
                   [Address] a,
                   [city] ct
                   WHERE c.Id_Address = a.Id
                   AND a.City = ct.Id

                SELECT 
                      c.ID    
                      ,c.Description   
                      ,c.RegisterDate   
                      FROM [City] c    
                      WHERE c.Description LIKE '%' + "sao paulo" + '%'