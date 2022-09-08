use ControleFinanceiro
go

create procedure ObterFluxoCaixa
as
begin

	DECLARE @Temp TABLE 
	(
	  Data Date, 
	  SaldoFinalDia Decimal(18,2)
	)

	DECLARE	@data datetime2,
			@tipo int,
			@saldoDia Decimal(18,2),
			@saldoFinal Decimal(18,2)

	set @saldoFinal = 0

	-- Cursor para percorrer os registros
	DECLARE cursor1 CURSOR FOR
		SELECT	Data
				,Tipo
				,sum(Valor)
		  FROM Lancamento
		  group by tipo, data
		  order by data

	--Abrindo Cursor
	OPEN cursor1

	-- Lendo a próxima linha
	FETCH NEXT FROM cursor1 INTO @data, @tipo, @saldoDia

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN

	if @tipo = 1
		set @saldoFinal = @saldoFinal + @saldoDia
	else
		set @saldoFinal = @saldoFinal - @saldoDia

	-- Executando as rotinas desejadas manipulando o registro
	insert into @Temp values(@data, @saldoFinal)
 
	-- Lendo a próxima linha
	FETCH NEXT FROM cursor1 INTO @data, @tipo, @saldoDia
	END
 
	-- Fechando Cursor para leitura
	CLOSE cursor1
 
	-- Finalizado o cursor
	DEALLOCATE cursor1

	select * from @Temp
 end