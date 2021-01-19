USE [PuntoDeVenta]
GO
/****** Object:  StoredProcedure [dbo].[SP_RESTAURANTE_ELIMINAR_PRODUCTO_MESA]    Script Date: 30/03/2020 01:50:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from sysobjects where name like 'SP_RESTAURANTE_ELIMINAR_PRODUCTO_MESA' and xtype = 'p')
	drop proc SP_RESTAURANTE_ELIMINAR_PRODUCTO_MESA
go

CREATE proc

	[dbo].[SP_RESTAURANTE_ELIMINAR_PRODUCTO_MESA]
	
	@codigo bigint,
	@numMesa int
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

				declare @ultimoProductoMesa int
				if exists(select top 1 * from PuntoDeVenta..TBL_RESTAURANTE_MESAS_PRODUCTOS where num_mesa = @numMesa and codigo_producto = @codigo)
				begin
					select top 1 @ultimoProductoMesa = id_mesa_producto from PuntoDeVenta..TBL_RESTAURANTE_MESAS_PRODUCTOS where num_mesa = @numMesa and codigo_producto = @codigo
					delete  from PuntoDeVenta..TBL_RESTAURANTE_MESAS_PRODUCTOS where ID_MESA_PRODUCTO = @ultimoProductoMesa
					update puntoDeventa..TBL_RESTAURANTE_PRODUCTOS set cantidad_existencia =cantidad_existencia+1 where codigo = @codigo
					select @estatus = 200, @mensaje = 'Producto eliminado'
				end
				
				else
				begin					
					select @estatus = -1, @mensaje = 'El producto ingresado no existe'					
				end
				
			
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
			else
			begin
				select @estatus estatus, @mensaje mensaje
			end
						
		end -- reporte de estatus
		
	end -- procedimiento
	grant exec on SP_RESTAURANTE_ELIMINAR_PRODUCTO_MESA to public
	go
