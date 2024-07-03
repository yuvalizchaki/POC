import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, TextField, Container, Typography, Box } from "@mui/material";
import { useCallback } from "react";
import { useAdminInfoContext } from "../../hooks/useAdminInfoContext";

const AdminLoginPage: React.FC = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const navigate = useNavigate();

  const { loginAdmin } = useAdminInfoContext();

  const handleLogin = useCallback(async () => {
    try {
      setIsLoading(true);
      setError(null);
      await loginAdmin(username, password);
      navigate("/admin/dashboard");
    } catch (error) {
      setError("Login failed. Please check your credentials and try again.");
    } finally {
      setIsLoading(false);
    }
  }, [loginAdmin, navigate, password, username]);

  return (
    <Container maxWidth="sm">
      <Box
        component="form"
        onSubmit={(e) => {
          e.preventDefault();
          e.stopPropagation();
          handleLogin();
        }}
        display="flex"
        flexDirection="column"
        alignItems="center"
        mt={8}
      >
        <Typography variant="h4" gutterBottom>
          Admin Login
        </Typography>
        <TextField
          label="Username"
          variant="outlined"
          fullWidth
          margin="normal"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          disabled={isLoading}
        />
        <TextField
          label="Password"
          type="password"
          variant="outlined"
          fullWidth
          margin="normal"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          disabled={isLoading}
        />
        {(error || isLoading) && (
          <Typography color={error ? "error" : "primary"} variant="body2">
            {error !== null ? error : isLoading ? "Loading..." : null}
          </Typography>
        )}
        <Button
          variant="contained"
          color="primary"
          // onClick={handleLogin}
          fullWidth
          sx={{ mt: 2 }}
          type="submit"
          disabled={isLoading}
        >
          Login
        </Button>
      </Box>
    </Container>
  );
};

export default AdminLoginPage;
