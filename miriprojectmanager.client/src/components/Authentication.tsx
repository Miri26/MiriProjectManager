import { Navigate } from "react-router-dom";

export const tokenCookieName = "ProjectManagerAccessToken";
export const usernameCookieName = "UsernameCookie"

export function getCookie(cookieName: string): string | null {
  const nameEQ = cookieName + "=";
  const cookies = document.cookie.split(";");
  for (let i = 0; i < cookies.length; i++) {
    let current_cookie = cookies[i].trim();
    if (current_cookie.indexOf(nameEQ) == 0)
      return current_cookie.substring(nameEQ.length, current_cookie.length);
  }
  return null;
}

export function setCookie(cookieName:string, value: string, days: number): void {
  let expires = "";
  if (days) {
    const date = new Date();
    date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
    expires = "; expires=" + date.toUTCString();
  }
    document.cookie = cookieName + "=" + value + expires + "; path=/";
}

export function deleteCookie(): void {
  document.cookie = tokenCookieName + "=; Max-Age=-99999999;";
}

const Authentication = () => {
    const cookieExists = getCookie(tokenCookieName);

  return !cookieExists ? (
    <Navigate to="/home" state={{ from: location }} replace />
  ) : (
    <Navigate to="/login" state={{ from: location }} replace />
  );
};

export default Authentication;
