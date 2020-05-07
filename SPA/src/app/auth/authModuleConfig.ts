import { environment } from 'src/environments/environment';
import { OAuthModuleConfig } from 'angular-oauth2-oidc';

export const authModuleConfig: OAuthModuleConfig = {
    resourceServer: {
      allowedUrls: [environment.api],
      sendAccessToken: true,
    }
  };