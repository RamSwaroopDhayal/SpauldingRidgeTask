USE [RAMTESTDB]
GO
/****** Object:  StoredProcedure [dbo].[usp_Sales]    Script Date: 30-04-2023 23:45:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_Sales]
@ActionType varchar(20)='',
@year varchar(5)='2018',
@percentage float =10.2,
@State varchar(50)='Alabama'
As
Begin
if @ActionType ='GetData'
Begin
select
	 sum(p.Sales)  as TotalSales
from 
	Products p join Orders o on p.OrderId = o.OrderId
where
	p.OrderId not in (select OrderId from OrdersReturns)
group by 
	year(o.OrderDate)
having 
	year(o.OrderDate) = @year 

select
	 sum(p.Sales)  as [Total_Sales],
	o.State
from 
	Products p join Orders o on p.OrderId = o.OrderId
where
	p.OrderId not in (select OrderId from OrdersReturns)
group by 
	year(o.OrderDate),
	o.State
having 
	year(o.OrderDate) = @year
Order by 
	o.State
End
if @ActionType ='Get_forecasted_Data'
Begin

select (@percentage * sum(p.Sales)) / 100 + sum(p.Sales) as forecasted_TotalSales
from 
	Products p join Orders o on p.OrderId = o.OrderId
where
	p.OrderId not in (select OrderId from OrdersReturns)
group by 
	year(o.OrderDate)
having 
	year(o.OrderDate) = @year 

	select
	 sum(p.Sales)  as [Total_Sales],(@percentage * sum(p.Sales)) / 100 + sum(p.Sales) as Percentage_increase,
	o.State
from 
	Products p join Orders o on p.OrderId = o.OrderId
where
	p.OrderId not in (select OrderId from OrdersReturns)
group by 
	year(o.OrderDate),
	o.State
having 
	year(o.OrderDate) = @year
Order by 
	o.State
End
else
Begin
	select
	 sum(p.Sales)  as [Total_Sales],(@percentage * sum(p.Sales)) / 100 + sum(p.Sales) as Percentage_increase,
	o.State
from 
	Products p join Orders o on p.OrderId = o.OrderId
where
	p.OrderId not in (select OrderId from OrdersReturns) and o.State=@State and year(o.OrderDate) = @year
group by 
	year(o.OrderDate),
	o.State
 
	
End

End
GO
