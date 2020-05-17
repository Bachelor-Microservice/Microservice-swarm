import { Query } from '@datorama/akita';
import { SessionStore, SessionState } from './SessionStore';

export class SessionQuery extends Query<SessionState> {  

    isLoggedIn = this.select(state => !!state.token);
    selectName = this.select('name');


  constructor(protected store: SessionStore) {
    super(store);
  }

}