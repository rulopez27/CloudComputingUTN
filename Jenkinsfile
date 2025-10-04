pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
    }

    stages {
        stage('Restore') {
            steps {
                sh 'dotnet restore CloudComputingUTN.sln'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build CloudComputingUTN.sln --configuration Release --no-restore'
            }
        }
        stage('Test') {
            steps {
                sh 'dotnet test CloudComputingUTN.sln --no-build --verbosity normal'
            }
        }
    }
    post {
        always {
            junit '**/TestResults/*.xml'
        }
    }
}