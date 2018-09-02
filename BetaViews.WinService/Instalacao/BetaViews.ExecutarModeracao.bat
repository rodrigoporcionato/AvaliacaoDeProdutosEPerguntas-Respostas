@ECHO ExecutarModeracaoService
PAUSE
sc delete BetaViews.Servicos.ExecutarModeracao
PAUSE
sc create BetaViews.Servicos.ExecutarModeracao binpath=D:\effectlab\projetos\BetaViews\dev\BetaViews\BetaViews.WinService\Instalacao\ModeracaoService\BetaViews.WinService.exe start=demand 
PAUSE