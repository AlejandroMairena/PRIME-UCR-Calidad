CREATE PROCEDURE [dbo].[GetIncidentsCounter]
(
	@modality		VARCHAR(30)
)
AS
BEGIN
	IF @modality = ''
	BEGIN
		SELECT COUNT(*)
		FROM Incidente I 
			JOIN Modalidad AS M ON I.Modalidad = M.Tipo
			JOIN Cita AS C ON C.Id = I.CodigoCita
		WHERE DATEDIFF(DAY,GETDATE(), C.FechaHoraEstimada) = 0;
		RETURN
	END
	ELSE
	BEGIN	
		SELECT COUNT(*)
		FROM Incidente I 
			JOIN Modalidad AS M ON I.Modalidad = M.Tipo
			JOIN Cita AS C ON C.Id = I.CodigoCita
		WHERE DATEDIFF(DAY,GETDATE(), C.FechaHoraEstimada) = 0 AND I.Modalidad = @modality;
		RETURN
	END
END;