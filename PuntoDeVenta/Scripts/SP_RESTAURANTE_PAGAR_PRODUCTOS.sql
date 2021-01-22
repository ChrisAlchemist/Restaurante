USE [PuntoDeVenta]
GO
/****** Object:  StoredProcedure [dbo].[SP_RESTAURANTE_PAGAR_PRODUCTOS]    Script Date: 30/03/2020 01:50:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from sysobjects where name like 'SP_RESTAURANTE_PAGAR_PRODUCTOS' and xtype = 'p')
	drop proc SP_RESTAURANTE_PAGAR_PRODUCTOS
go

CREATE proc

	[dbo].[SP_RESTAURANTE_PAGAR_PRODUCTOS]
	
	@ticketVenta varchar(12)


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
						@numMesa int,
						@numVenta bigint

					
			end -- inicio
			
			begin -- ámbito de la actualización

				select @numMesa = num_mesa, @numVenta = NUM_VENTA from TBL_RESTAURANTE_TICKETS_VENTA where TICKET = @ticketVenta

				if @numMesa is not null
				begin
					
					insert into
					TBL_RESTAURANTE_PRODUCTOS_VENDIDOS 
					(
						NUM_MESA,
						TICKET_VENTA,
						CODIGO_PRODUCTO,
						PRECIO_PRODUCTO,
						FECHA_ALTA
					)
					SELECT NUM_MESA, TICKET_VENTA, CODIGO_PRODUCTO,PRECIO_PRODUCTO,getdate()
					FROM TBL_RESTAURANTE_MESAS_PRODUCTOS where TICKET_VENTA = @ticketVenta

					update TBL_RESTAURANTE_MESAS_PRODUCTOS set pagado = 1 where num_mesa = @numMesa and TICKET_VENTA= @ticketVenta
					update TBL_RESTAURANTE_MESAS set RESERVADA =0, ASIGNADA =0, TOTAL_CUENTA = 0, HORA_ASIGNACION =null  where NUM_MESA = @numMesa

					select @estatus=200, @mensaje = 'se ha cobrado la cuenta con exito'

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
	grant exec on SP_RESTAURANTE_PAGAR_PRODUCTOS to public
	go
