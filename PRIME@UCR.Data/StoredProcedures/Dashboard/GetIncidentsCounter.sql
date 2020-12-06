CREATE PROCEDURE [dbo].[GetIncidentsCounter]
(
	@modality		VARCHAR(30),
	@filter			VARCHAR(30)
)
AS
BEGIN
	IF @modality = ''
	BEGIN
		IF @filter = 'Día'
		BEGIN
			SELECT COUNT(*)
			FROM Incidente I 
				JOIN Modalidad AS M ON I.Modalidad = M.Tipo
				JOIN Cita AS C ON C.Id = I.CodigoCita
			WHERE DATEDIFF(DAY,GETDATE(), C.FechaHoraEstimada) = 0;
			RETURN
		END
		IF @filter = 'Semana'
		BEGIN
			SELECT COUNT(*)
			FROM Incidente I 
				JOIN Modalidad AS M ON I.Modalidad = M.Tipo
				JOIN Cita AS C ON C.Id = I.CodigoCita
			WHERE DATEDIFF(DAY,GETDATE(), C.FechaHoraEstimada) <= 6;
			RETURN
		END
		IF @filter = 'Mes'
		BEGIN
			SELECT COUNT(*)
			FROM Incidente I 
				JOIN Modalidad AS M ON I.Modalidad = M.Tipo
				JOIN Cita AS C ON C.Id = I.CodigoCita
			WHERE DATEDIFF(DAY,GETDATE(), C.FechaHoraEstimada) <= 29;
			RETURN
		END
		IF @filter = 'Año'
		BEGIN
			SELECT COUNT(*)
			FROM Incidente I 
				JOIN Modalidad AS M ON I.Modalidad = M.Tipo
				JOIN Cita AS C ON C.Id = I.CodigoCita
			WHERE DATEDIFF(DAY,GETDATE(), C.FechaHoraEstimada) <= 364;
			RETURN
		END
	END
	ELSE
	BEGIN	
		IF @filter = 'Día'
		BEGIN 
			SELECT COUNT(*)
			FROM Incidente I 
				JOIN Modalidad AS M ON I.Modalidad = M.Tipo
				JOIN Cita AS C ON C.Id = I.CodigoCita
			WHERE DATEDIFF(DAY,GETDATE(), C.FechaHoraEstimada) = 0 AND I.Modalidad = @modality;
			RETURN
		END
		IF @filter = 'Semana'
		BEGIN 
			SELECT COUNT(*)
			FROM Incidente I 
				JOIN Modalidad AS M ON I.Modalidad = M.Tipo
				JOIN Cita AS C ON C.Id = I.CodigoCita
			WHERE DATEDIFF(DAY,GETDATE(), C.FechaHoraEstimada) <= 6 AND I.Modalidad = @modality;
			RETURN
		END
		IF @filter = 'Mes'
		BEGIN 
			SELECT COUNT(*)
			FROM Incidente I 
				JOIN Modalidad AS M ON I.Modalidad = M.Tipo
				JOIN Cita AS C ON C.Id = I.CodigoCita
			WHERE DATEDIFF(DAY,GETDATE(), C.FechaHoraEstimada) <= 29 AND I.Modalidad = @modality;
			RETURN
		END
		IF @filter = 'Año'
		BEGIN 
			SELECT COUNT(*)
			FROM Incidente I 
				JOIN Modalidad AS M ON I.Modalidad = M.Tipo
				JOIN Cita AS C ON C.Id = I.CodigoCita
			WHERE DATEDIFF(DAY,GETDATE(), C.FechaHoraEstimada) <= 364 AND I.Modalidad = @modality;
			RETURN
		END
	END
END;