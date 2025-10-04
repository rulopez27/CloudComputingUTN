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
        stage('Test and Coverage') {
            steps {
                sh 'dotnet test --no-build --verbosity normal --collect:"XPlat Code Coverage" --logger:"nunit;LogFilePath=TestResults/test-results.xml"'
            }
        }
    }
    post {
        always {
            nunit allowEmptyResults: true, testResults: '**/TestResults/test-results.xml'
            recordCoverage tools: [[parser: 'COBERTURA', pattern: '**/coverage.cobertura.xml']]
        }
    }
}