USE [PuntoDeVenta]
GO
/****** Object:  StoredProcedure [dbo].[SP_RESTAURANTE_EDITAR_PRODUCTO]    Script Date: 30/03/2020 01:50:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from sysobjects where name like 'SP_RESTAURANTE_EDITAR_PRODUCTO' and xtype = 'p')
	drop proc SP_RESTAURANTE_EDITAR_PRODUCTO
go

CREATE proc

	[dbo].[SP_RESTAURANTE_EDITAR_PRODUCTO]
	@numProducto bigint,
	@codigo bigint,
	@nombreProducto varchar(max),
	@precio money,
	@descripcionProducto varchar(max),
	@editar bit,
	@elimiar bit,
	@precioCompra money,
	@cantidadExistencia bigint
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
				if @elimiar = 1
				begin
					update TBL_RESTAURANTE_PRODUCTOS set ACTIVO = 0 where ID_PRODUCTO = @numProducto	
					select @estatus = 200, @mensaje = concat('Producto ',@numProducto, ' eliminado')
				end
				else
				if @editar = 1
				begin
					update TBL_RESTAURANTE_PRODUCTOS 
					set 
						CODIGO = @codigo, 
						NOMBRE_PRODUCTO = @nombreProducto ,
						PRECIO = @precio,
						DESCRIPCION_PRODUCTO = @descripcionProducto,
						PRECIO_DE_COMPRA = @precioCompra,
						CANTIDAD_EXISTENCIA =@cantidadExistencia
					where ID_PRODUCTO= @numProducto
					select @estatus = 200, @mensaje = 'Producto editado con exito'
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
	grant exec on SP_RESTAURANTE_EDITAR_PRODUCTO to public
	go
