import { InformarInstrumentoFinanceiroComponent } from './componentes/informar-instrumento-financeiro/InformarInstrumentoFinanceiroComponent';
import { CorrecaoBaseComponent } from './componentes/correcaoBase/CorrecaoBaseComponent';
import { DiaNaoUtilGuard } from 'app/guards//DiaNaoUtilGuard';
import { DataProcessoComponent } from './componentes/dataProcesso/DataProcessoComponent';
import { InstituicaoService } from './services/instituicao/InstituicaoService';
import { HomeComponent } from './componentes/common/home/HomeComponent';
import { HomeConsultaComponent } from './componentes/homeConsulta/HomeConsultaComponent';
import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { CoreErrorComponent } from 'frontend-components/lib/components/core/error';
import { AuthenticationGuard } from "frontend-components/lib/components/core/security";
import { InstituicaoComponent } from "app/componentes/instituicao/InstituicaoComponent";
import { InstituicaoSelecionadaGuard } from "app/guards/InstituicaoSelecionadaGuard";


export const routes: Routes = [
    {
        path: 'home',
        redirectTo: ''
    },
    {
        path: '',
        component: HomeComponent,
        pathMatch: "full",
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard],
        canActivateChild: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'error',
        component: CoreErrorComponent
    },
    {
        path: 'boletas',
        loadChildren: 'app/modules/boleta/lancamento/LancamentoBoletasModule#LancamentoBoletasModule',
        data: { "menuId": "lancamento_boletas" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard, DiaNaoUtilGuard]
    },
    {
        path: 'boletas',
        loadChildren: 'app/modules/boleta/batimento/GerenciarBatimentosModule#GerenciarBatimentosModule',
        data: { "menuId": "gerenciar_batimentos" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard, DiaNaoUtilGuard]
    },
    {
        path: 'importar/boleta',
        loadChildren: 'app/modules/importacao-boleta/ImportacaoBoletaModule#ImportacaoBoletaModule',
        data: { "menuId": "importarboleta" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'instituicao', component: InstituicaoComponent,
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'dataProcesso', component: DataProcessoComponent,
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'home-consulta',
        component: HomeConsultaComponent,
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'papel',
        loadChildren: 'app/modules/papel/PapelModule#PapelModule',
        data: { "menuId": "gerenciador_papel" },
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'produto',
        loadChildren: 'app/modules/produto/ProdutoModule#ProdutoModule',
        data: { "menuId": "gerenciador_produto" },
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'area',
        loadChildren: 'app/modules/area/AreaModule#AreaModule',
        data: { "menuId": "gerenciador_area" },
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'grupo-contabil',
        loadChildren: 'app/modules/grupo-contabil/GrupoContabilModule#GrupoContabilModule',
        data: { "menuId": "gerenciador_grupo_contabil" },
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'emissor',
        loadChildren: 'app/modules/emissor/EmissorModule#EmissorModule',
        data: { "menuId": "gerenciador_emissor" },
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'operacoes',
        loadChildren: 'app/modules/operacao/OperacaoModule#OperacaoModule',
        data: { "menuId": "gerenciar_operacoes" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard,DiaNaoUtilGuard]
    },
    {
        path: 'operacoes/carenciaresgate',
        loadChildren: 'app/modules/operacao/CarenciaResgateCrudModule#CarenciaResgateCrudModule',
        data: { "menuId": "carencia_resgate" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'conciliacaoanaliticacetip',
        loadChildren: 'app/modules/cetip/ConciliacaoCetipModule#ConciliacaoCetipModule',
        data: { "menuId": "conciliacaoanaliticacetip" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'consultas',
        loadChildren: 'app/modules/consultas/ConsultaEmissaModule#ConsultaEmissaModule',
        data: { "menuId": "assistente_consultas" },
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'exportacoes',
        loadChildren: 'app/modules/exportacoes/ExportacoesModule#ExportacoesModule',
        data: { "menuId": "exportacoes" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'dashboard',
        loadChildren: 'app/modules/dashboard/DashBoardModule#DashBoardModule',
        data: { "menuId": "dashboard" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'boletas',
        loadChildren: 'app/modules/boleta/conglomerado/EfetivarConglomeradoModule#EfetivarConglomeradoModule',
        data: { "menuId": "efetivar_conglomerado" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard, DiaNaoUtilGuard]
    },
    {
        path: 'investidor',
        loadChildren: 'app/modules/investidor/InvestidorModule#InvestidorModule',
        data: { "menuId": "gerenciador_investidor" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'correcao-base',
        component: CorrecaoBaseComponent,
        pathMatch: "full",
        canActivate: [AuthenticationGuard],
        canActivateChild: [AuthenticationGuard]
    },
    {
        path:'operacoes/informar-instrumento-financeiro',
        loadChildren: 'app/modules/informar-instrumento-financeiro/InformarInstrumentoFinanceiroModule#InformarInstrumentoFinanceiroModule',
        data: { "menuId": "instrumento_financeiro" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path:'integracao',
        loadChildren: 'app/modules/configuracoes/integracao/ConfiguracaoIntegracaoModule#ConfiguracaoIntegracaoModule',
        data: { "menuId": "integracao" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'liquidacaocc',
        loadChildren: 'app/modules/operacao/LiquidacaoCCModule#LiquidacaoCCModule',
        data: { "menuId": "liquidacao_cc" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path:'configuracao',
        loadChildren: 'app/modules/configuracoes/instituicao/ConfiguracaoInstituicaoModule#ConfiguracaoInstituicaoModule',
        data: { "menuId": "configuracao-instituicao" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path:'tarefas',
        loadChildren: 'app/modules/importacao/ImportacaoModule#ImportacaoModule',
        data: {"menuId": "tarefas"},
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    },
    {
        path: 'configuracao',
        loadChildren: 'app/modules/configuracoes/processo-tarefas/ConfiguracaoImportacaoModule#ConfiguracaoImportacaoModule',
        data: { "menuId": "configuracao-tarefas" },
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'simulador-investimento',
        loadChildren: 'app/modules/simulador-investimento/SimuladorInvestimentoModule#SimuladorInvestimentoModule',
        data: { "menuId": "simulador-investimento" },
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'ajustar-resgate',
        loadChildren: 'app/modules/ajustar-resgate/AjustarResgateModule#AjustarResgateModule',
        data: { "menuId": "ajustar-resgate" },
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'apurarsaldoindividual',
        loadChildren: 'app/modules/ferramentas/saldo/ApuracaoSdoIndividualModule#ApuracaoSdoIndividualModule',
        data: { "menuId": "apurarsaldoind" },
        canActivate: [AuthenticationGuard, InstituicaoSelecionadaGuard]
    }
];
export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
