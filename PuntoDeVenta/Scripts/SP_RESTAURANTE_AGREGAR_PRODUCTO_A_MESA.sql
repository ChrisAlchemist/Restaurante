USE [PuntoDeVenta]
GO
/****** Object:  StoredProcedure [dbo].[SP_RESTAURANTE_AGREGAR_PRODUCTO_A_MESA]    Script Date: 30/03/2020 01:50:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from sysobjects where name like 'SP_RESTAURANTE_AGREGAR_PRODUCTO_A_MESA' and xtype = 'p')
	drop proc SP_RESTAURANTE_AGREGAR_PRODUCTO_A_MESA
go

CREATE proc

	[dbo].[SP_RESTAURANTE_AGREGAR_PRODUCTO_A_MESA]
	
	@numMesa int,
	@codigoProducto bigint

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
						@mensaje varchar(max) = '',		
						@precioProducto money

					
			end -- inicio
			
			begin -- ámbito de la actualización
				
				if exists (select 1 from PuntoDeVenta..TBL_RESTAURANTE_MESAS where NUM_MESA = @numMesa and asignada =1)
				begin

					if exists (select 1 from PuntoDeVenta..TBL_RESTAURANTE_PRODUCTOS where codigo = @codigoProducto)
					begin

						if exists(select 1 from PuntoDeVenta..TBL_RESTAURANTE_PRODUCTOS where codigo = @codigoProducto and  CANTIDAD_EXISTENCIA>0)
						begin
							select @precioProducto = precio from PuntoDeVenta..TBL_RESTAURANTE_PRODUCTOS where codigo = @codigoProducto 
							insert into PuntoDeVenta..TBL_RESTAURANTE_MESAS_PRODUCTOS(NUM_MESA,CODIGO_PRODUCTO, PRECIO_PRODUCTO,FECHA_ALTA,PAGADO)
							values (@numMesa,@codigoProducto,@precioProducto,GETDATE(), 0) 
							update PuntoDeVenta..TBL_RESTAURANTE_PRODUCTOS set CANTIDAD_EXISTENCIA = CANTIDAD_EXISTENCIA-1 where codigo = @codigoProducto
							select @estatus = 200, @mensaje = 'Agregado exitosamente.'
						end
						else
						begin
							select @estatus = -1, @error_message = 'No hay suficientes productos en existencia.'
						end
					end
					else
					begin
						select @estatus = -1, @error_message = 'El producto ingresado no existe.'
					end

				end
				else
				begin					
					select @estatus = -1, @error_message = 'El numero de mesa ingresado ya existe.'					
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
	grant exec on SP_RESTAURANTE_AGREGAR_PRODUCTO_A_MESA to public
	go
