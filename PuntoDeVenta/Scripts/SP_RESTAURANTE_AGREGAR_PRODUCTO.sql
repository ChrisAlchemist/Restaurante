USE [PuntoDeVenta]
GO
/****** Object:  StoredProcedure [dbo].[SP_RESTAURANTE_AGREGAR_PRODUCTO]    Script Date: 30/03/2020 01:50:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from sysobjects where name like 'SP_RESTAURANTE_AGREGAR_PRODUCTO' and xtype = 'p')
	drop proc SP_RESTAURANTE_AGREGAR_PRODUCTO
go

CREATE proc

	[dbo].[SP_RESTAURANTE_AGREGAR_PRODUCTO]
	
	@codigo bigint,
	@nombreProducto varchar(max),
	@descripcionProducto varchar(max),
	@precio money


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

				if not exists (select 1 from PuntoDeVenta..TBL_RESTAURANTE_PRODUCTOS where CODIGO = @codigo)
				begin
					insert into PuntoDeVenta..TBL_RESTAURANTE_PRODUCTOS(CODIGO,NOMBRE_PRODUCTO, DESCRIPCION_PRODUCTO,FECHA_ALTA,PRECIO, ACTIVO)
					values (@codigo,@nombreProducto,@descripcionProducto,GETDATE(), @precio,  1) 

					select @estatus = 200, @mensaje = 'Producto agregado exitosamente.'

				end
				else
				begin
					if exists (select 1 from PuntoDeVenta..TBL_RESTAURANTE_PRODUCTOS where CODIGO = @codigo and ACTIVo = 0)
					begin
						update 
							PuntoDeVenta..TBL_RESTAURANTE_PRODUCTOS 
						SET 
							CODIGO = @codigo, 
							NOMBRE_PRODUCTO = @nombreProducto, 
							DESCRIPCION_PRODUCTO = @descripcionProducto,
							FECHA_ALTA = GETDATE(), 
							PRECIO = @precio, 													
							ACTIVO = 1
						WHERE 
							CODIGO = @codigo
						and 
							ACTIVO = 0
							
						select @estatus = 200, @mensaje = 'Producto agregado exitosamente.'	
					end
					else
					begin
						select @estatus = -1, @error_message = 'El codigo de producto ingresado ya existe.'
					end
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
	grant exec on SP_RESTAURANTE_AGREGAR_PRODUCTO to public
	go
