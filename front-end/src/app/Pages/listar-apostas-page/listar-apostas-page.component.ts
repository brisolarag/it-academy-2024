import { Component } from '@angular/core';
import { SorteioService } from '../../Services/Requests/sorteio.service';
import { StatusService } from '../../Services/status.service';

interface Aposta {
  id: number;
  nome: string;
  cpf: string;
  data: string;
}

@Component({
  selector: 'app-listar-apostas-page',
  standalone: true,
  imports: [],
  templateUrl: './listar-apostas-page.component.html',
  styleUrl: './listar-apostas-page.component.scss'
})
export class ListarApostasPageComponent {

  constructor(private service:SorteioService, private status:StatusService) {}

  apostas: Aposta[] = [];
  sorteioAtivo: any;
  currentStatus: any;

  ngOnInit() {
    this.service.getTodasApostasAtivas().subscribe({
      next: (res) => {
        if (res) {
          this.apostas = res.map(aposta => {
            return {
              ...aposta,
              data: this.formatarData(aposta.data)
            }
          })
        } else {
          this.apostas = [];
        }
      },
      error: (error) => {
        console.error('Error fetching the data:', error)
        this.apostas = [];
      }
    })

    this.service.getSorteioAtivo().subscribe({
      next: (res) => {
        if (res) {
          this.sorteioAtivo = res;
          this.currentStatus = this.status.getStatus(res.status);
        } else {
          this.sorteioAtivo = undefined;
          this.currentStatus == undefined
        }
      },
      error: (error) => {
        console.error('Error fetching data:', error);
        this.sorteioAtivo = undefined;
        this.currentStatus == undefined
      }
    });

  }

  


  private formatarData(data: string): string {
    const dataObj = new Date(data);
    const dia = dataObj.getDate().toString().padStart(2, '0'); 
    const mes = (dataObj.getMonth() + 1).toString().padStart(2, '0'); 
    const ano = dataObj.getFullYear().toString();
    const hora = dataObj.getHours().toString().padStart(2, '0');
    const minuto = dataObj.getMinutes().toString().padStart(2, '0');
    const segundo = dataObj.getSeconds().toString().padStart(2, '0');
    return `${dia}/${mes}/${ano} | ${hora}:${minuto}:${segundo}`;
  }
}


