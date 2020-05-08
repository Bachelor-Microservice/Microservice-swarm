import { AuthConfig } from 'angular-oauth2-oidc';
import { environment } from 'src/environments/environment';


export const authConfig: AuthConfig = {
    issuer:  environment.identity,
    clientId: 'spa',
    redirectUri: window.location.origin + '',
    silentRefreshRedirectUri: window.location.origin + '',
    postLogoutRedirectUri: window.location.origin,
    responseType: 'code',
    scope: 'openid profile',
    strictDiscoveryDocumentValidation: false,
    silentRefreshTimeout: 500000, // For faster testing
     // For faster testing
     skipIssuerCheck: true,
     requireHttps:  false,
    sessionChecksEnabled: true,
    showDebugInformation: false, // Also requires enabling "Verbose" level in devtools
    clearHashAfterLogin: false, // https://github.com/manfredsteyer/angular-oauth2-oidc/issues/457#issuecomment-431807040
  };