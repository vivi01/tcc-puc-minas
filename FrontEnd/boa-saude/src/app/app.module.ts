import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MenuComponent } from './menu/menu.component';
import { AssociadoComponent } from './associado/associado.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AssociadosService } from './service/associado.service';
import { MatRadioModule} from '@angular/material/radio'; 
import { MatButtonModule} from '@angular/material/button'; 
import { MatDatepickerModule} from '@angular/material/datepicker'; 
import { MatNativeDateModule} from '@angular/material/core'; 
import { MatIconModule} from '@angular/material/icon'; 
import { MatCardModule} from '@angular/material/card'; 
import { MatSidenavModule} from '@angular/material/sidenav'; 
import { MatFormFieldModule} from '@angular/material/form-field'; 
import { MatInputModule} from '@angular/material/input'; 
import { MatTooltipModule} from '@angular/material/tooltip'; 
import { MatToolbarModule} from '@angular/material/toolbar'; 
import { MatMenuModule} from '@angular/material/menu'; 
import { LayoutModule } from '@angular/cdk/layout';
import { MatListModule } from '@angular/material/list';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { PlanosService } from './service/plano.service';
import { AssociadoResolve } from './resolvers/associadoResolve';
import { PlanoResolve } from './resolvers/planoResolve';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    AssociadoComponent    
  ],
  imports: [
    BrowserModule,
    FormsModule, 
    ReactiveFormsModule,  
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,  
    MatMenuModule,  
    MatDatepickerModule,  
    MatNativeDateModule,  
    MatIconModule,  
    MatRadioModule,  
    MatCardModule,  
    MatSidenavModule,  
    MatFormFieldModule,  
    MatInputModule,  
    MatTooltipModule,  
    MatToolbarModule,  
    AppRoutingModule, LayoutModule, MatListModule  ,
    RouterModule,
    MatTableModule
  ],
  providers: [
    HttpClientModule, AssociadosService, PlanosService, MatDatepickerModule,
    AssociadoResolve, PlanoResolve
  ],    
  bootstrap: [AppComponent]
})
export class AppModule { }
