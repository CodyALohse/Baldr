import { Component, OnInit } from '@angular/core';
import { Institution } from './institution.model';
import { InstitutionService } from './institution.service';

@Component({
    selector: 'institution-list',
    templateUrl: './institution-list.component.html',
    providers: [InstitutionService],
    styles: []
})
export class InstitutionListComponent implements OnInit {

    public institution: Institution;
    public errorMessage: string;

    constructor(private institutionService: InstitutionService) { }

    ngOnInit() {
        this.getInstitutions();
    }

    public getInstitutions() {
        this.institutionService.getInstitution()
            .subscribe(
            institution => this.institution = <Institution>institution,
            error => this.errorMessage = <any>error
            );
    }

}