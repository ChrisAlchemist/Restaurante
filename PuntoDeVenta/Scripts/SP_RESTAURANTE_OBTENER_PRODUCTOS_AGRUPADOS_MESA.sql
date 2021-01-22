USE [PuntoDeVenta]
GO
/****** Object:  StoredProcedure [dbo].[SP_RESTAURANTE_OBTENER_PRODUCTOS_AGRUPADOS_MESA]    Script Date: 30/03/2020 01:50:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from sysobjects where name like 'SP_RESTAURANTE_OBTENER_PRODUCTOS_AGRUPADOS_MESA' and xtype = 'p')
	drop proc SP_RESTAURANTE_OBTENER_PRODUCTOS_AGRUPADOS_MESA
go

CREATE proc

	[dbo].[SP_RESTAURANTE_OBTENER_PRODUCTOS_AGRUPADOS_MESA]
	
	@numMesa bigint	
as

	begin -- procedimiento
		
		begin try -- try principal
		
			begin -- inicio

				-- declaraciones
				declare @estatus int = 200,
						@error_message varchar(255) = '',
						@error_line varchar(255) = '',
						@error_severity varchar(255) = '',
						@error_procedure varchar(255) = '',
						@mensaje varchar(max) = ''						

					
			end -- inicio
			
			begin -- ámbito de la actualización

				select 
					mp.NUM_MESA,
					mp.CODIGO_PRODUCTO,
					mp.PRECIO_PRODUCTO,
					
					p.NOMBRE_PRODUCTO,
					m.NOMBRE_MESA,
					count(mp.CODIGO_PRODUCTO) cantidad_productos,
					sum(mp.precio_producto) total_productos,
					(select sum(precio_producto) from TBL_RESTAURANTE_MESAS_PRODUCTOS where num_mesa =mp.num_mesa and pagado =0) TOTAL_MESA,
					mp.TICKET_VENTA

				from 
					TBL_RESTAURANTE_MESAS_PRODUCTOS mp
					join TBL_RESTAURANTE_PRODUCTOS p
					on p.CODIGO = mp.CODIGO_PRODUCTO

					join TBL_RESTAURANTE_MESAS m
					on m.NUM_MESA = mp.NUM_MESA
				where
					mp.NUM_MESA = @numMesa
				and
					mp.PAGADO = 0	
				group by
					mp.NUM_MESA,
					mp.CODIGO_PRODUCTO,
					mp.PRECIO_PRODUCTO,	
					p.NOMBRE_PRODUCTO,
					m.NOMBRE_MESA,
					mp.TICKET_VENTA
			
			end -- ámbito de la actualización

		end try -- try principal
		
		begin catch -- catch principal
		
			-- captura del error
			select	@estatus = -error_state(),
					@error_procedure = coalesce(error_procedure(), 'CONSULTA DINÁMICA'),
					@error_line = error_line(),
					@error_message = error_message(),
					@error_severity =
						case error_severity()
							when 11 then 'Error en validación'
							when 12 then 'Error en consulta'
							when 13 then 'Error en actualización'
							else 'Error general'
						end
		
		end catch -- catch principal
		
		begin -- reporte de estatus
			if @error_message<>''
			begin
				select	@estatus estatus,
						@error_procedure error_procedure,
						@error_line error_line,
						@error_severity error_severity,
						@error_message error_message
			end
						
		end -- reporte de estatus
		
	end -- procedimiento
	grant exec on SP_RESTAURANTE_OBTENER_PRODUCTOS_AGRUPADOS_MESA to public
	go
