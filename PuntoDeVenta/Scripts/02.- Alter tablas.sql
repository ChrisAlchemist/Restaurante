use PuntoDeVenta
go

if not exists (select * from information_schema.columns where table_name like 'TBL_RESTAURANTE_MESAS' and column_name like 'ACTIVA')
	alter table TBL_RESTAURANTE_MESAS add ACTIVA bit 
go