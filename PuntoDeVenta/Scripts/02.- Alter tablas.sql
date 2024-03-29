use PuntoDeVenta
go

if not exists (select * from information_schema.columns where table_name like 'TBL_RESTAURANTE_MESAS' and column_name like 'ACTIVA')
	alter table TBL_RESTAURANTE_MESAS add ACTIVA bit 
go


if not exists (select * from information_schema.columns where table_name like 'TBL_RESTAURANTE_PRODUCTOS' and column_name like 'CANTIDAD_EXISTENCIA')
	alter table TBL_RESTAURANTE_PRODUCTOS add CANTIDAD_EXISTENCIA BIGINT
go

if not exists (select * from information_schema.columns where table_name like 'TBL_RESTAURANTE_PRODUCTOS' and column_name like 'PRECIO_DE_COMPRA')
	alter table TBL_RESTAURANTE_PRODUCTOS add PRECIO_DE_COMPRA MONEY
go


if not exists (select * from information_schema.columns where table_name like 'TBL_RESTAURANTE_MESAS_PRODUCTOS' and column_name like 'TICKET_VENTA')
	alter table TBL_RESTAURANTE_MESAS_PRODUCTOS add TICKET_VENTA varchar(12)
go

--alter table TBL_RESTAURANTE_MESAS_PRODUCTOS drop column TICKET_VENTA 

select * from TBL_RESTAURANTE_PRODUCTOS