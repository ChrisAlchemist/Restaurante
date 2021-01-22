USE [PuntoDeVenta]
GO
/****** Object:  StoredProcedure [dbo].[SP_RESTAURANTE_EDITAR_MESA]    Script Date: 30/03/2020 01:50:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from sysobjects where name like 'SP_RESTAURANTE_EDITAR_MESA' and xtype = 'p')
	drop proc SP_RESTAURANTE_EDITAR_MESA
go

CREATE proc

	[dbo].[SP_RESTAURANTE_EDITAR_MESA]
	@numMesa int,
	@nombreMesa varchar(max),
	@reservar bit,
	@editar bit,
	@elimiar bit
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
					update TBL_RESTAURANTE_MESAS set ACTIVA = 0 where NUM_MESA = @numMesa	
					select @estatus = 200, @mensaje = concat('Mesa ',@numMesa, ' eliminada')
				end
				else
				if @editar = 1
				begin
					update TBL_RESTAURANTE_MESAS set NOMBRE_MESA = @nombreMesa where NUM_MESA = @numMesa	
					select @estatus = 200, @mensaje = 'Mesa editada con exito'
				end
				else
				begin
					if exists (select 1 from PuntoDeVenta..TBL_RESTAURANTE_MESAS where NUM_MESA = @numMesa)
					begin
						update TBL_RESTAURANTE_MESAS set NOMBRE_MESA = @nombreMesa, RESERVADA = @reservar, HORA_ASIGNACION = GETDATE(), ASIGNADA = 1 where NUM_MESA = @numMesa

						select @estatus = 200
						if @reservar =1
							set @mensaje = 'Mesa reservada con exito'
						else
							set @mensaje = 'Mesa asignada con exito'
						
						declare @ticket varchar(12)
						select @ticket = concat((REPLACE(STR('', 11-len(@ticket)), SPACE(1), '0')) , case when  max(ID_TICKET) is null then 1 else max(ID_TICKET) +1 end)
						from TBL_RESTAURANTE_TICKETS_VENTA
				
						insert into PuntoDeVenta..TBL_RESTAURANTE_TICKETS_VENTA(TICKET,NUM_MESA, NUM_VENTA,FECHA_ALTA, TICKET_CERRADO)
						values (@ticket,@numMesa,null,GETDATE(), 0) 

					end
					else
					begin
						select @estatus = -1, @mensaje = 'La mesa ingresada no existe'
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
	grant exec on SP_RESTAURANTE_EDITAR_MESA to public
	go
