import { NumberFormatPipe } from 'frontend-components/lib/components/lang';
import { InstituicaoService } from './services/instituicao/InstituicaoService';
import { SelectItem } from 'primeng/primeng';
import { ManagedMethod } from 'frontend-components/lib/components/lang';
import { Instituicao } from './model/common/instituicao/Instituicao';
import { LocalDate } from 'frontend-components/lib/components/lang';
import { DataService } from './services/common/DataService';
import { Component, ElementRef, ViewChild, OnInit } from '@angular/core';

import { Router, ActivatedRoute } from '@angular/router';
import { SecurityService, UsuarioSessaoService, Usuario } from "frontend-components/lib/components/core/security";
import { CookieService } from "ng2-cookies";
import { StorageService } from "frontend-components/lib/components/core/persistence";
import { Subscription } from "rxjs/Subscription";
import { Subject } from "rxjs/Subject";
import { HomeComponent } from './componentes/common/home/HomeComponent';
import { Cookie } from 'ng2-cookies/ng2-cookies';

declare var Ultima: any;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {

    classUser: string = "";

    layoutCompact: boolean = true;

    layoutMode: string = 'overlay';

    darkMenu: boolean = false;

    exibirInativas: boolean = true;

    profileMode: string = 'top';

    instituicaoSelecionada: Instituicao = null;

    temInstituicaoSelecionada: boolean = false;

    exibirModalDiaNaoUtil = false;
    
    private onLoginSucessSubscription: Subscription;

    private numberFormatPipe = new NumberFormatPipe();

    usuario: Usuario = new Usuario();

    url: string = window.location.origin;

    codatividade: number = 19071;

    get dataSelecionada(): Date {
        if (this.instituicaoSelecionada && this.instituicaoSelecionada.dataSelecionada) {
            return this.instituicaoSelecionada.dataSelecionada.toDate();
        }
        return new Date( Date.now() );
    }

    get diaSelecionado(): string {
        return this.numberFormatPipe.transform(this.dataSelecionada.getUTCDate(), "2.0-0" );
    }

    get mesSelecionado(): string {
        return this.numberFormatPipe.transform(this.dataSelecionada.getUTCMonth() + 1, "2.0-0" );
    }

    constructor(private el: ElementRef,
        private activatedRoute: ActivatedRoute,
        private securityService: SecurityService,
        private usuarioSessaoService: UsuarioSessaoService,
        private dataService: DataService,
        private instituicaoService: InstituicaoService,
        private router: Router) {
            this.securityService.definirSistemaCorrente("EMI");
        activatedRoute.queryParams.subscribe(params => {
            if (params['reloadHeader']) {
                if (this.instituicaoService.temInstituicaoSelecionada()) {
                    this.instituicaoSelecionada = this.instituicaoService.obterInstituicao();
                }
                this.temInstituicaoSelecionada = true;
            }
            if (params['reloadUser']) {
                if(this.usuarioSessaoService.getUsuario())
                this.usuario = this.usuarioSessaoService.getUsuario();
            }
            if (params['exibirDashBoard']) {
                 this.router.navigateByUrl("/dashboard");
            }
        });
    }

    public onActivate(event){
        if(event instanceof HomeComponent){
            this.classUser = "user-item active-top-menu";
        } else {
            this.classUser = "";
        }
    }

    public initUltimaLib(){
        Ultima.init( this.el.nativeElement );
    }

    ngAfterViewInit() {
        this.initUltimaLib();
    }

    public isAuthenticated(): boolean {
        try {
            return this.securityService.isAuthenticated();            
        } catch (error) {
            console.log(error);
            return false;
        }
    }

    public logout() {
        try {
            this.securityService.logout().subscribe({
                next: (fezLogout) => {
                    if (fezLogout) {
                        this.securityService.isOpenId().subscribe({
                            next: (isOpenId) => {
                                if(isOpenId) {
                                    this.securityService.getPathLogout().subscribe({
                                        next: (path) => {
                                            Cookie.delete('userNameCookie');
                                            Cookie.delete('JSESSIONID');
                                            Cookie.delete('OAuth_Token_Request_State');
                                            window.location.href = path + "/protocol/openid-connect/logout?redirect_uri=" + this.url + "/emissao";
                                        }
                                    })
                                } else {
                                    this.router.navigateByUrl('/seguranca/autenticacao/login')
                                }
                            }
                        })
                    }
                },
                error: (error) => {
                    console.log(error);
                }
            });
        } catch (error) {
            console.log(error);
        }
    }

    abrirTelaInstituicao() {
        this.router.navigate(["/instituicao"], { queryParams: { abrirTela: true } });
    }

    abrirTelaData() {
        this.router.navigateByUrl("/dataProcesso");
    }

}
