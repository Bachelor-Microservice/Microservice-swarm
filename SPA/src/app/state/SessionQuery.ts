import { Query, toBoolean } from '@datorama/akita';
import { filter, map } from 'rxjs/operators';
import { SessionState, SessionStore } from './SessionStore';


export class SessionQuery extends Query<SessionState> {

 

    constructor(protected store: SessionStore) {
        super(store);
    }

    
  
}