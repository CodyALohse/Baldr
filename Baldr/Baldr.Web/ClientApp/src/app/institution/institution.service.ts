import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { Institution } from './institution.model';
import { LoggerService } from 'core/services/logger.service';



export class InstitutionService {

    //private institutionUrl = 'http://localhost:51377/v1/institutions/1';

    //constructor(private http: Http, private log: LoggerService) { }

    //public getInstitution(): Observable<any> {
    //    return this.http.get(this.institutionUrl)
    //        .map(this.extractData)
    //        .catch(this.handleError);
    //}

    //private extractData(res: Response): Institution {
    //    return res.json() as Institution;
    //    //return (body.data || {}) as Institution;
    //}

    //private handleError(error: Response | any){
    //    let errMsg: string;
    //    if (error instanceof Response) {
    //        const body = error.json() || '';
    //        const err = body.error || JSON.stringify(body);
    //        errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    //    }
    //    else {
    //        errMsg = error.message ? error.message : error.toString();
    //    }

    //    this.log.error(errMsg);
    //    return Observable.throw(errMsg);
    //}

}