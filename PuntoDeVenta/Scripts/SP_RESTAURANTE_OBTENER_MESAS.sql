USE [PuntoDeVenta]
GO
/****** Object:  StoredProcedure [dbo].[SP_RESTAURANTE_OBTENER_MESAS]    Script Date: 30/03/2020 01:50:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from sysobjects where name like 'SP_RESTAURANTE_OBTENER_MESAS' and xtype = 'p')
	drop proc SP_RESTAURANTE_OBTENER_MESAS
go

CREATE proc

	[dbo].[SP_RESTAURANTE_OBTENER_MESAS]
	
	@asiganda bit	
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
			
			begin -- �mbito de la actualizaci�n

				select 
					ID_MESA,
					NUM_MESA,
					NOMBRE_MESA,
					RESERVADA,
					FECHA_ALTA,
					HORA_ASIGNACION,
					TOTAL_CUENTA,
					ASIGNADA
				from 
					TBL_RESTAURANTE_MESAS
				where
					ASIGNADA = @asiganda 
				and
					ACTIVA = 1	
				order by FECHA_ALTA
			
			end -- �mbito de la actualizaci�n

		end try -- try principal
		
		begin catch -- catch principal
		
			-- captura del error
			select	@estatus = -error_state(),
					@error_procedure = coalesce(error_procedure(), 'CONSULTA DIN�MICA'),
					@error_line = error_line(),
					@error_message = error_message(),
					@error_severity =
						case error_severity()
							when 11 then 'Error en validaci�n'
							when 12 then 'Error en consulta'
							when 13 then 'Error en actualizaci�n'
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
	grant exec on SP_RESTAURANTE_OBTENER_MESAS to public
	go