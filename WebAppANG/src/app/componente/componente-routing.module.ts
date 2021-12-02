import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CintaListComponent } from './cinta-list/cinta-list.component';

const routes: Routes = [
  {
    path: '', component: CintaListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ComponenteRoutingModule { }
