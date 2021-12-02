import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ComponenteRoutingModule } from './componente-routing.module';
import { CintaListComponent } from './cinta-list/cinta-list.component';


@NgModule({
  declarations: [
    CintaListComponent
  ],
  imports: [
    CommonModule,
    ComponenteRoutingModule
  ]
})
export class ComponenteModule { }
